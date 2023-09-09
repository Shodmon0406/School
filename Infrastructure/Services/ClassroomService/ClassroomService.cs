using System.Net;
using AutoMapper;
using Domain.Dtos.Classroom;
using Domain.Entities;
using Domain.Filters.ClassroomFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClassroomService;

public class ClassroomService : IClassroomService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClassroomService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResponse<List<GetClassroomDto>>> GetClassroom(GetClassroomFilter filter)
    {
        try
        {
            var room = _context.Classrooms.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Description))
                room = room.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
            var response = await room.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();
            var mapped = _mapper.Map<List<GetClassroomDto>>(room);
            var totalRecord = room.Count();
            return new PagedResponse<List<GetClassroomDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetClassroomDto>>(HttpStatusCode.NotFound, e.Message);
        }
    }

    public async Task<Response<GetClassroomDto>> GetClassroomById(int classId)
    {
        try
        {
            var room = await _context.Classrooms.FirstOrDefaultAsync(x => x.ClassroomId == classId);
            if (room == null)
            {
                return new Response<GetClassroomDto>(HttpStatusCode.BadRequest, "not found");
            }

            var mapped = _mapper.Map<GetClassroomDto>(room);
            return new Response<GetClassroomDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetClassroomDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassroomDto>> AddClassroom(AddClassroomDto? addClassroom)
    {
        try
        {
            if (addClassroom == null)
            {
                return new Response<GetClassroomDto>(HttpStatusCode.BadRequest, "Please fill out this field");
            }

            var room = _mapper.Map<Classroom>(addClassroom);
            await _context.Classrooms.AddRangeAsync(room);
            await _context.SaveChangesAsync();
            var mapped = _mapper.Map<GetClassroomDto>(room);
            return new Response<GetClassroomDto>(HttpStatusCode.OK, "ok", mapped);
        }
        catch (Exception e)
        {
            return new Response<GetClassroomDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassroomDto>> UpdateClassroom(AddClassroomDto? updateClassroom)
    {
        if (updateClassroom == null)
        {
            return new Response<GetClassroomDto>(HttpStatusCode.BadRequest, "Please fill out this field");
        }

        var claasRoom = await _context.Classrooms.FirstOrDefaultAsync(x => x.ClassroomId == updateClassroom.ClassroomId);
        if (claasRoom == null)
        {
            return new Response<GetClassroomDto>(HttpStatusCode.BadRequest, "not found");
        }

        var room = _mapper.Map<Classroom>(updateClassroom);
        _context.Classrooms.Update(room);
        await _context.SaveChangesAsync();
        var mapped = _mapper.Map<GetClassroomDto>(room);
        return new Response<GetClassroomDto>(HttpStatusCode.OK, "ok", mapped);
    }

    public async Task<Response<bool>> DeleteClassroom(int classId)
    {
        try
        {
            var existing = await _context.Classrooms.FindAsync(classId);
            if (existing == null)
            {
                return new Response<bool>(HttpStatusCode.BadRequest, "not found this classroom");
            }

            _context.Classrooms.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<bool>(HttpStatusCode.OK, "ok");
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
   
}