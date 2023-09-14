using System.Net;
using AutoMapper;
using Domain.Dtos.Teacher;
using Domain.Entities;
using Domain.Filters.TeacherFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TeacherServices;

public class TeacherService : ITeacherService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TeacherService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetTeacherDto>>> GetTeacherAsync(GetTeacherFilter filter)
    {
        try
        {
            var teacher = _context.Teachers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                teacher = teacher.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                teacher = teacher.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            var response = await teacher.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await teacher.CountAsync();
            var mapped = _mapper.Map<List<GetTeacherDto>>(response);

            return new PagedResponse<List<GetTeacherDto>>(mapped, totalRecord, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTeacherDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTeacherDto>> GetTeacherByIdAsync(int id)
    {
        try
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return new Response<GetTeacherDto>(HttpStatusCode.BadRequest, "Teacher not found");
            var mapped = _mapper.Map<GetTeacherDto>(teacher);
            return new Response<GetTeacherDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetTeacherDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTeacherDto>> CreateTeacherAsync(AddTeacherDto addTeacher)
    {
        try
        {
            var add = await _context.Teachers.FirstOrDefaultAsync(x => x.TeacherCode == addTeacher.TeacherCode);
            if (add != null)
            {
                return new Response<GetTeacherDto>(HttpStatusCode.BadRequest, "Teacher  Code expected");
            }

            var teacher = _mapper.Map<Teacher>(addTeacher);
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetTeacherDto>(teacher);
            return new Response<GetTeacherDto>(mapped);
            
        }
        catch (Exception e)
        {
            return new Response<GetTeacherDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetTeacherDto>> UpdateTeacherAsync(AddTeacherDto updateTeacher)
    {
        try
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x=>x.TeacherId==updateTeacher.TeacherId);
            if (teacher == null) return new Response<GetTeacherDto>(HttpStatusCode.BadRequest, "Teacher not found");
            var map = _mapper.Map<Teacher>(teacher);
            _context.Teachers.Update(map);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetTeacherDto>(teacher);
            return new Response<GetTeacherDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetTeacherDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteTeacherAsync(int id)
    {
        try
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return new Response<bool>(HttpStatusCode.BadRequest, "Teacher not found");
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}