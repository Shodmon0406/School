using System.Net;
using AutoMapper;
using Domain.Dtos.Class;
using Domain.Entities;
using Domain.Filters.ClassFilter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.ClassService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Claas;

public class ClassService : IClassService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClassService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<List<GetClassDto>>> GetClass(GetClassFilter filter)
    {
        try
        {
            var name = _context.Classes.AsQueryable();
            if (!string.IsNullOrEmpty(filter.ClassName))
                name = name.Where(l =>
                    l.ClassName.ToLower().Contains(filter.ClassName.ToLower()));

            var response = await name.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var mapped = _mapper.Map<List<GetClassDto>>(response);
            var totalRecord = name.Count();
            return new PagedResponse<List<GetClassDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetClassDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassDto>> GetClassById(int classId)
    {
        try
        {
            var name = await _context.Classes.FirstOrDefaultAsync(x => x.ClassId == classId);
            if (name != null)
            {
                var result = _mapper.Map<GetClassDto>(name);
                return new Response<GetClassDto>(result);
            }

            return new Response<GetClassDto>(HttpStatusCode.BadRequest, "not found this class");
        }
        catch (Exception e)
        {
            return new Response<GetClassDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassDto>> AddClass(AddClassDto? addClass)
    {
        try
        {
            if (addClass == null)
            {
                return new Response<GetClassDto>(HttpStatusCode.NotFound, "Please fill out this field");
            }

            var add = _mapper.Map<Class>(addClass);
            await _context.Classes.AddAsync(add);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetClassDto>(add);

            return new Response<GetClassDto>(HttpStatusCode.OK, "ok",mapped);
        }
        catch (Exception e)
        {
            return new Response<GetClassDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassDto>> UpdateClass(AddClassDto? updateClass)
    {
        try
        {
            if (updateClass == null)
            {
                return new Response<GetClassDto>(HttpStatusCode.NotFound, "Please fill out this field");
            }

            var name = await _context.Classes.FirstOrDefaultAsync(x => x.ClassId == updateClass.ClassId);
            
            if (name == null)
            {
                return new Response<GetClassDto>(HttpStatusCode.BadRequest, "not found this class");
            }
            var update = _mapper.Map<Class>(updateClass);
            _context.Classes.Update(update);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetClassDto>(update);

            return new Response<GetClassDto>(HttpStatusCode.OK, "ok",mapped);
        }
        catch (Exception e)
        {
            return new Response<GetClassDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteClass(int classId)
    {
        try
        {
            var found = await _context.Classes.FirstOrDefaultAsync(x => x.ClassId == classId);
            if (found==null)
            {
                return new Response<bool>(HttpStatusCode.NotFound, "Not found this class");
            }

            _context.Classes.Remove(found);
            await _context.SaveChangesAsync();

            return new Response<bool>(true);

        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}