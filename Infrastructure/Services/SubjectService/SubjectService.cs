using System.Net;
using AutoMapper;
using Domain.Dtos.Student;
using Domain.Entities;
using Domain.Filters.SubjectFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SubjectService;

public class SubjectService : ISubjectService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SubjectService(DataContext context, IMapper mapper)
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

            var response = await subject.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await subject.CountAsync();
            var mapped = _mapper.Map<List<GetSubjectDto>>(response);

            return new PagedResponse<List<GetSubjectDto>>(mapped, totalRecord, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetSubjectDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubjectDto>> GetSubjectByIdAsync(int id)
    {
        try
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(x => x.SubjectId == id);
            if (result == null)
            {
                return new Response<GetSubjectDto>(HttpStatusCode.BadRequest,
                    "Subject id is not specified in the request ");
            }

            var mapped = _mapper.Map<GetSubjectDto>(result);
            return new Response<GetSubjectDto>(HttpStatusCode.OK, "ok", mapped);
        }
        catch (Exception e)
        {
            return new Response<GetSubjectDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubjectDto>> CreateSubjectAsync(AddSubjectDto addSubject)
    {
        try
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(x => x.SubjectName == addSubject.SubjectName);
            if (result != null)
            {
                return new Response<GetSubjectDto>(HttpStatusCode.BadRequest,
                    "Subject name is already exist");
            }

            var mapped = _mapper.Map<Subject>(addSubject);
            await _context.Subjects.AddAsync(mapped);
            await _context.SaveChangesAsync();
            var result1 = _mapper.Map<GetSubjectDto>(mapped);
            return new Response<GetSubjectDto>(HttpStatusCode.OK, "ok", result1);
        }
        catch (Exception e)
        {
            return new Response<GetSubjectDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetSubjectDto>> UpdateSubjectAsync(AddSubjectDto addSubject)
    {
        try
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(x => x.SubjectName == addSubject.SubjectName);
            if (result == null && result.SubjectId != addSubject.SubjectId)
            {
                return new Response<GetSubjectDto>(HttpStatusCode.BadRequest,
                    "Subject name is already exist");
            }
            else if (result != null && result.SubjectId == addSubject.SubjectId)
            {
                var mapped = _mapper.Map<Subject>(addSubject);
                _context.Subjects.Update(mapped);
                await _context.SaveChangesAsync();
                var result1 = _mapper.Map<GetSubjectDto>(mapped);
                return new Response<GetSubjectDto>(HttpStatusCode.OK, "ok", result1);
            }

            return new Response<GetSubjectDto>(HttpStatusCode.BadRequest, " Bad request for add subject ");
        }
        catch (Exception e)
        {
            return new Response<GetSubjectDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteSubjectAsync(int id)
    {
        try
        {
            var result = await _context.Subjects.FirstOrDefaultAsync(x => x.SubjectId == id);
            if (result == null)
            {
                return new Response<bool>(HttpStatusCode.BadRequest,
                    "Subject id is not specified in the request ");
            }

            _context.Subjects.Remove(result);
            await _context.SaveChangesAsync();
            return new Response<bool>(HttpStatusCode.OK, "ok", true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}