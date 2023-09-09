using System.Net;
using AutoMapper;
using Domain.Dtos.Student;
using Domain.Entities;
using Domain.Filters.StudentFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GetStudentDto = Domain.Dtos.Student.GetStudentDto;

namespace Infrastructure.Services.StudentServices;

public class StudentService : IStudentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StudentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<List<GetStudentDto>>> GetStudents(GetStudentFilter filter)
    {
        try
        {
            var students = _context.Students.AsQueryable();
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                students = students.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                students = students.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                students = students.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            var response = await students.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var totalRecord = await students.CountAsync();
            var mapped = _mapper.Map<List<GetStudentDto>>(response);
            return new PagedResponse<List<GetStudentDto>>(mapped, totalRecord, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetStudentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStudentDto>> GetStudentById(string studentId)
    {
        try
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student not found");
            var mapped = _mapper.Map<GetStudentDto>(student);
            return new Response<GetStudentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStudentDto>> CreateStudent(AddStudentDto? student)
    {
        try
        {

            var existing = await _context.Students.FirstOrDefaultAsync(x => x.StudentCode == student.StudentCode);
            if (existing!=null)
            {
                return new Response<GetStudentDto>(HttpStatusCode.BadRequest, "Student already exists");
            }
            
            
            var newStudent = _mapper.Map<Student>(student);
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetStudentDto>(newStudent);
            return new Response<GetStudentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStudentDto>> UpdateStudent(AddStudentDto? student)
    {
        try
        {
            if (student == null)
                return new Response<GetStudentDto>(HttpStatusCode.BadRequest, "student is required to update");
            
            var existing = await _context.Students.FindAsync(student.Id);
            if(existing==null) return new Response<GetStudentDto>(HttpStatusCode.NotFound, "student  is not found");
            
            // var mapped = _mapper.Map<Student>(student);
            if (student.FirstName == null) existing.FirstName = existing.FirstName;
            existing.FirstName = student.FirstName;
            if (student.LastName == null) existing.LastName = existing.LastName;
            existing.LastName = student.LastName;
            if (student.Email==null) existing.Email=existing.Email;
            existing.Email = student.Email;
            if(student.Active==null) existing.Active=existing.Active;
            existing.Active = student.Active;
            if(student.StudentCode==null) existing.StudentCode=existing.StudentCode;
            existing.StudentCode = student.StudentCode;
            if(student.Gender==null) existing.Gender=existing.Gender;  
            existing.Gender=student.Gender;
            if (student.Stage == null) existing.Stage = existing.Stage;                
            existing.Stage=student.Stage;
            if(student.CreatedAt==null) existing.CreatedAt=existing.CreatedAt;
            existing.CreatedAt=student.CreatedAt;
            if(student.UpdateAt==null) existing.UpdateAt=existing.UpdateAt;
            existing.UpdateAt=student.UpdateAt;
            if(student.JoinDate==null) existing.JoinDate=existing.JoinDate;
            existing.JoinDate=student.JoinDate;
            if(student.Id==null) existing.Id=existing.Id;
            existing.Id=student.Id;
            if(student.PhoneNumber==null) existing.PhoneNumber=existing.PhoneNumber;
            existing.PhoneNumber=student.PhoneNumber;
            if (student.DOB == null) existing.DOB =existing.DOB;
            existing.DOB = student.DOB;
            if(student.Address==null) existing.Address=existing.Address;
            existing.Address=student.Address;
             _context.Students.Update(existing);
             await _context.SaveChangesAsync();;
             var response = _mapper.Map<GetStudentDto>(existing);
             return new Response<GetStudentDto>(response);
        }
        catch (Exception e)
        {
            return new Response<GetStudentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteStudent(string studentId)
    {
        try
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return new Response<bool>(HttpStatusCode.BadRequest, "Student not found for deletion",false);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
            
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}