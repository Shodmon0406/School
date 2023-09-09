using Domain.Dtos.Teacher;
using Domain.Filters.TeacherFilter;
using Domain.Responses;

namespace Infrastructure.Services.TeacherServices;

public  interface ITeacherService
{
    Task<Response<List<GetTeacherDto>>> GetTeacherAsync(GetTeacherFilter filter);
    Task<Response<GetTeacherDto>> GetTeacherByIdAsync(int id);
    Task<Response<GetTeacherDto>> CreateTeacherAsync(AddTeacherDto addTeacher);
    Task<Response<GetTeacherDto>> UpdateTeacherAsync(AddTeacherDto updateTeacher);
    Task<Response<bool>> DeleteTeacherAsync(int id);

}