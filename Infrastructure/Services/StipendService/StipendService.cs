using AutoMapper;
using Domain.Dtos.StipendDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Services.StipendService
{
    public class StipendService : IStipendService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StipendService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateStipendAsync(AddStipendDto stipend)
        {
            try
            {
                var mapped = _mapper.Map<Stipend>(stipend);
                mapped.CreatedAt = DateTime.UtcNow;
                await _context.Stipends.AddAsync(mapped);
                await _context.SaveChangesAsync();
                return new Response<int>(mapped.Id);
            }
            catch (Exception e)
            {
                return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<bool>> DeleteStipendAsync(int id)
        {
            try
            {
                var stipend = await _context.Stipends.FindAsync(id);
                if (stipend == null) return new Response<bool>(false);
                _context.Stipends.Remove(stipend);
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<GetStipendDto>> GetStipendAsync(int id)
        {
            try
            {
                var stipend = await _context.Stipends.FindAsync(id);
                if (stipend == null) return new Response<GetStipendDto>(HttpStatusCode.BadRequest, "Stipend not found!");
                var mapped = _mapper.Map<GetStipendDto>(stipend);
                return new Response<GetStipendDto>(mapped);
            }
            catch (Exception e)
            {
                return new Response<GetStipendDto>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<List<GetStipendDto>>> GetStipendsAsync()
        {
            try
            {
                var stipends = await _context.Stipends.ToListAsync();
                var mapped = _mapper.Map<List<GetStipendDto>>(stipends);
                return new Response<List<GetStipendDto>>(mapped);
            }
            catch (Exception e)
            {
                return new Response<List<GetStipendDto>>(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public async Task<Response<int>> UpdateStipendAsync(AddStipendDto stipend)
        {
            try
            {
                var mapped = _mapper.Map<Stipend>(stipend);
                mapped.UpdateAt = DateTime.UtcNow;
                _context.Stipends.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<int>(mapped.Id);
            }
            catch (Exception e)
            {
                return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
