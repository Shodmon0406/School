using Domain.Dtos.Classroom;
using Domain.Filters.ClassroomFilter;
using Infrastructure.Services.ClassroomService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassroomController:Controller
{
    private readonly IClassroomService _classroomService;

    public ClassroomController(IClassroomService classroomService)
    {
        _classroomService = classroomService;
    }

    [HttpGet("get/classrooms")]
    public async Task<IActionResult> GetClassrooms(GetClassroomFilter classroomFilter)
    {
        if (ModelState.IsValid)
        {
            var classrooms = await _classroomService.GetClassroom(classroomFilter);
            return StatusCode(classrooms.StatusCode, classroomFilter);
        }
        
        return BadRequest(ModelState);
    }

    [HttpGet("get/classroom/by/id")]
    public async Task<IActionResult> GetClassroom(int id)
    {
        if (ModelState.IsValid)
        {
            var classroom = await _classroomService.GetClassroomById(id);
            return StatusCode(classroom.StatusCode, classroom);
        }
        
        return BadRequest(ModelState);
    }

    [HttpPost("create/classroom")]
    public async Task<IActionResult> CreateClassroom(AddClassroomDto createClassroomDto)
    {
        if (ModelState.IsValid)
        {
            var classroom = await _classroomService.AddClassroom(createClassroomDto);
            return StatusCode(classroom.StatusCode, classroom);
        }
        
        return BadRequest(ModelState);
    }

    [HttpPut("update/classroom")]
    public async Task<IActionResult> UpdateClassroom(AddClassroomDto updateClassroomDto)
    {
        if (ModelState.IsValid)
        {
            var classroom = await _classroomService.UpdateClassroom(updateClassroomDto);
            return StatusCode(classroom.StatusCode, classroom);
        }
        
        return BadRequest(ModelState);
    }

    [HttpDelete("delete/classroom")]
    public async Task<IActionResult> DeleteClassroom(int id)
    {
        if (ModelState.IsValid)
        {
            var classroom = await _classroomService.DeleteClassroom(id);
            return StatusCode(classroom.StatusCode, classroom);
        }
        
        return BadRequest(ModelState);
    }
    
}