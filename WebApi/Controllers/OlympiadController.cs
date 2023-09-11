using Domain.Dtos.OlympiadDtos;
using Domain.Filters.OlympiadFilter;
using Domain.Responses;
using Infrastructure.Services.OlympiadService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OlympiadController : ControllerBase
{
    private readonly IOlympiadService _olympiadService;
    public OlympiadController(IOlympiadService olympiadService)
    {
        _olympiadService = olympiadService;
    }

    [HttpGet("GetAllOlympiad")]
    async public Task<Response<List<GetOlympiadDto>>> GetAllOlympiads(GetOlympiadFilter filter)
    {
        return await GetAllOlympiads(filter);
    }

[HttpGet("GetOlympiadById")]
    async public Task<Response<GetOlympiadDto>> GetOlympiadById(int id)
    {
        return await GetOlympiadById(id);
    }

    [HttpPost("AddOlympiad")]
    async public Task<Response<GetOlympiadDto>> AddOlympiad(AddOlympiadDto olympiad)
    {
        return await AddOlympiad(olympiad);
    }

    [HttpPut]
    async public Task<Response<GetOlympiadDto>> UpdateOlympiad(AddOlympiadDto olympiad)
    {
        return await UpdateOlympiad(olympiad);
    }
    async public Task<Response<bool>> DeleteOlympiad(int id)
    {
        return await DeleteOlympiad(id);
    }
}
