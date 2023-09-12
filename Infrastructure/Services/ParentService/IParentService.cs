using Domain.Dtos;
using Domain.Filters.ParentFilter;
using Domain.Responses;

namespace Infrastructure.Services.ParentService.cs;

public interface IParentService
{
    Task<Response<List<GetParentDto>>> GetAllParents(GetParentFilter filter);
    Task<Response<GetParentDto>> GetParentById(int id);
    Task<Response<GetParentDto>> AddParent(AddParentDto parent);
    Task<Response<GetParentDto>> UpdateParent(AddParentDto parent);
    Task<Response<bool>> DeleteParent(int id);
}
