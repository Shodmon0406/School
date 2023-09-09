using Domain.Dtos.Class;
using Domain.Filters.ClassFilter;
using Domain.Responses;

namespace Infrastructure.Services.ClassService;

public interface IClassService
{
    Task<PagedResponse<List<GetClassDto>>> GetClass(GetClassFilter filter);
    Task<Response<GetClassDto>> GetClassById(int classId);
    Task<Response<GetClassDto>> AddClass(AddClassDto addClass);
    Task<Response<GetClassDto>> UpdateClass(AddClassDto updateClass);
    Task<Response<bool>> DeleteClass(int classId);
}