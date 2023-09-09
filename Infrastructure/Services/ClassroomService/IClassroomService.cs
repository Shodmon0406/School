using Domain.Dtos.Class;
using Domain.Dtos.Classroom;
using Domain.Filters.ClassFilter;
using Domain.Filters.ClassroomFilter;
using Domain.Responses;

namespace Infrastructure.Services.ClassroomService;

public interface IClassroomService
{
    Task<PagedResponse<List<GetClassroomDto>>> GetClassroom(GetClassroomFilter filter);
    Task<Response<GetClassroomDto>> GetClassroomById(int classId);
    Task<Response<GetClassroomDto>> AddClassroom(AddClassroomDto addClassroom);
    Task<Response<GetClassroomDto>> UpdateClassroom(AddClassroomDto updateClassroom);
    Task<Response<bool>> DeleteClassroom(int classId);
}