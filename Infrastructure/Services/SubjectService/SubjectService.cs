
using System.Net;
using AutoMapper;
using Domain.Dtos.Student;
using Domain.Filters.SubjectFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SubjectService;

public class SubjectService:ISubjectService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SubjectService(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PagedResponse<List<GetSubjectDto>>> GetSubjectAsync(GetSubjectFilter filter)
    {
        try
        {
            var subject = _context.Subjects.AsQueryable();
            if (!string.IsNullOrEmpty(filter.SubjectName))
            {
                subject = subject.Where(x => x.SubjectName.ToLower().Contains(filter.SubjectName.ToLower()));
            }
            
            var response  = await subject.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await subject.CountAsync();
            var mapped = _mapper.Map<List<GetSubjectDto>>(response);
            
            return new PagedResponse<List<GetSubjectDto>>(mapped, totalRecord,filter.PageNumber,filter.PageSize);
            
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetSubjectDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubjectDto>> GetSubjectByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<GetSubjectDto>> CreateSubjectAsync(AddStudentDto addStudent)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<GetSubjectDto>> UpdateSubjectAsync(AddStudentDto updateStudent)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<bool>> DeleteSubjectAsync(int id)
    {
        throw new NotImplementedException();
    }
}