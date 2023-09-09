using Domain.Dtos.Teacher;
using Domain.Filters.TeacherFilter;
using Infrastructure.Services.TeacherServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
public class TeacherController: Controller
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet("get/teachers")]
    public async Task<IActionResult> GetTeachers([FromQuery]GetTeacherFilter filter)
    {
        if (ModelState.IsValid)
        {
            var teacher = await _teacherService.GetTeacherAsync(filter);
            return StatusCode(teacher.StatusCode, teacher);
        }
        return BadRequest(ModelState);
    }

    [HttpGet("get/teacher/by/id")]
    public async Task<IActionResult> GetTeacher([FromQuery]int id)
    {
        if (ModelState.IsValid)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            return StatusCode(teacher.StatusCode, teacher);
        }
        return BadRequest(ModelState);
    }

    [HttpPost("create/teacher")]
    public async Task<IActionResult> CreateTeacher([FromBody] AddTeacherDto teacher)
    {
        if (ModelState.IsValid)
        {
            var result = await _teacherService.CreateTeacherAsync(teacher);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("update/teacher")]
    public async Task<IActionResult> UpdateTeacher([FromBody] AddTeacherDto teacher)
    {
        if (ModelState.IsValid)
        {
            var result = await _teacherService.UpdateTeacherAsync(teacher);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("delete/teacher")]
    public async Task<IActionResult> DeleteTeacher([FromQuery] int id)
    {
        if (ModelState.IsValid)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

}