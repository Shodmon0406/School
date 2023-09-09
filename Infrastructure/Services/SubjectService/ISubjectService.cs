using Domain.Dtos.Student;
using Domain.Filters.SubjectFilter;
using Domain.Responses;

namespace Infrastructure.Services.SubjectService;

public  interface ISubjectService
{
    Task<PagedResponse<List<GetSubjectDto>>> GetSubjectAsync(GetSubjectFilter filter);
    Task<Response<GetSubjectDto>> GetSubjectByIdAsync(int id);
    Task<Response<GetSubjectDto>> CreateSubjectAsync(AddStudentDto addStudent);
    Task<Response<GetSubjectDto>> UpdateSubjectAsync(AddStudentDto updateStudent);
    Task<Response<bool>> DeleteSubjectAsync(int id);
}