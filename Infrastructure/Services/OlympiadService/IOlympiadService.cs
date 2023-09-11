using Domain.Dtos.OlympiadDtos;
using Domain.Responses;
using Domain.Filters.OlympiadFilter;

namespace Infrastructure.Services.OlympiadService;

public interface IOlympiadService
{
    Task<Response<List<GetOlympiadDto>>> GetAllOlympiads(GetOlympiadFilter filter);
    Task<Response<GetOlympiadDto>> GetOlympiadById(int id);
    Task<Response<GetOlympiadDto>> AddOlympiad(AddOlympiadDto Olympiad);
    Task<Response<GetOlympiadDto>> UpdateOlympiad(AddOlympiadDto Olympiad);
    Task<Response<bool>> DeleteOlympiad(int id);
}
