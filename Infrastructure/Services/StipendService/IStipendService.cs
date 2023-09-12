using Domain.Dtos.StipendDto;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Components.Web;
using Org.BouncyCastle.Crypto.Engines;

namespace Infrastructure.Services.StipendService
{
    public interface IStipendService
    {
        Task<Response<List<GetStipendDto>>> GetStipendsAsync();
        Task<Response<GetStipendDto>> GetStipendAsync(int id);
        Task<Response<int>> CreateStipendAsync(AddStipendDto stipend);
        Task<Response<int>> UpdateStipendAsync(AddStipendDto stipend);
        Task<Response<bool>> DeleteStipendAsync(int id);
    }
}
