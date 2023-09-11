using AutoMapper;
using Domain.Dtos.OlympiadDtos;
using Domain.Responses;
using Domain.Filters.OlympiadFilter;
using Infrastructure.Data;
using Domain.Entities;

namespace Infrastructure.Services.OlympiadService;

public class OlympiadService : IOlympiadService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public OlympiadService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    async public Task<Response<List<GetOlympiadDto>>> GetAllOlympiads(GetOlympiadFilter filter)
    {
        var olympiads = _dataContext.Olympiads.AsQueryable();
        var totalRecords = olympiads.Count();
        var mapper = _mapper.Map<List<GetOlympiadDto>>(olympiads);
        return new Response<List<GetOlympiadDto>>(mapper);

    }
    async public Task<Response<GetOlympiadDto>> GetOlympiadById(int id)
    {
        var olympiad = await _dataContext.Olympiads.FindAsync(id);
        if (olympiad == null)
        {
            return new Response<GetOlympiadDto>(System.Net.HttpStatusCode.BadRequest, "Olimpiad no found");
        }
        var mapped = _mapper.Map<GetOlympiadDto>(olympiad);
        return new Response<GetOlympiadDto>(mapped);
    }
    async public Task<Response<GetOlympiadDto>> AddOlympiad(AddOlympiadDto olympiad)
    {
        var mapped = _mapper.Map<Olympiad>(olympiad);
        await _dataContext.Olympiads.AddAsync(mapped);
        await _dataContext.SaveChangesAsync();
        var result = _mapper.Map<GetOlympiadDto>(olympiad);
        return new Response<GetOlympiadDto>(result);
    }
    async public Task<Response<GetOlympiadDto>> UpdateOlympiad(AddOlympiadDto olympiad)
    {
        var query = _dataContext.Olympiads.AsQueryable().Where(o => o.Id == olympiad.Id).First();
        var mapped = _mapper.Map<Olympiad>(query);
        _dataContext.Olympiads.AddAsync(mapped);
        _dataContext.SaveChangesAsync();
        var result = _mapper.Map<GetOlympiadDto>(olympiad);
        return new Response<GetOlympiadDto>(System.Net.HttpStatusCode.OK, "Update is succesfully");
    }
    async public Task<Response<bool>> DeleteOlympiad(int id)
    {
        var query = await _dataContext.Olympiads.FindAsync(id);
        if (query == null)
        {
            return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Olimpiad not found");
        }
        _dataContext.Olympiads.Remove(query);
        await _dataContext.SaveChangesAsync();
        return new Response<bool>(System.Net.HttpStatusCode.OK, "Olimpiad was delete successfully");

    }
}

