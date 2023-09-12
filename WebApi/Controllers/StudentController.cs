using Domain.Dtos.Student;
using Domain.Filters.ClassFilter;
using Domain.Filters.StudentFilter;
using Infrastructure.Services.StudentServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentController:Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet("get/student")]
    public async Task<IActionResult> GetStudents([FromQuery]GetStudentFilter filter)
    {
        if (ModelState.IsValid)
        {
            var result = await _studentService.GetStudents(filter);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpGet("get/student/by/id")]
    public async Task<IActionResult> GetStudentById([FromQuery]string studentId)
    {
        if (ModelState.IsValid)
        {
            var result = await _studentService.GetStudentById(studentId);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpPost("create/student")]
    public async Task<IActionResult> CreateStudent([FromBody] AddStudentDto student)
    {
        if (ModelState.IsValid)
        {
            var result = await _studentService.CreateStudent(student);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("update/student")]
    public async Task<IActionResult> UpdateStudent([FromBody] AddStudentDto student)
    {
        if (ModelState.IsValid)
        {
            var result = await _studentService.UpdateStudent(student);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("delete/student")]
    public async Task<IActionResult> DeleteStudent([FromQuery]string studentId)
    {
        if (ModelState.IsValid)
        {
            var result = await _studentService.DeleteStudent(studentId);
            return StatusCode(result.StatusCode, result);
        }
        return BadRequest(ModelState);
    }

}