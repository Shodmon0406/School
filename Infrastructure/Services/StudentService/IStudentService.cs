using Domain.Dtos.Student;
using Domain.Filters.StudentFilter;
using Domain.Responses;

namespace Infrastructure.Services.StudentServices;

public interface IStudentService
{
    Task<PagedResponse<List<GetStudentDto>>> GetStudents(GetStudentFilter filter);
    Task<Response<GetStudentDto>> GetStudentById(string studentId);
    Task<Response<GetStudentDto>> CreateStudent(AddStudentDto student);
    Task<Response<GetStudentDto>> UpdateStudent(AddStudentDto student);
    Task<Response<bool>> DeleteStudent(string studentId);
}