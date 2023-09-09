using Domain.Dtos.Student;
using Domain.Filters.SubjectFilter;
using Infrastructure.Services.SubjectService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController:Controller
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService  subjectService)
    {
        _subjectService = subjectService;
    }
    
    [HttpGet("get/subjects")]
    public async Task<IActionResult> GetSubjects([FromQuery]GetSubjectFilter filter)
    {
        if (ModelState.IsValid)
        {
            var subject = await _subjectService.GetSubjectAsync(filter);
            return StatusCode(subject.StatusCode, subject);
        }
        return BadRequest(ModelState);
    }

    [HttpGet("get/subject/id")]
    public async Task<IActionResult> GetSubject([FromQuery]int id)
    {
        if (ModelState.IsValid)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            return StatusCode(subject.StatusCode, subject);
        }
        return BadRequest(ModelState);
    }

    [HttpPost("create/subject")]
    public async Task<IActionResult> CreateSubject([FromBody]AddSubjectDto subjectDto)
    {
        if (ModelState.IsValid)
        {
            var subject = await _subjectService.CreateSubjectAsync(subjectDto);
            return StatusCode(subject.StatusCode, subject);
        }
        return BadRequest(ModelState);
    }

    [HttpPut("update/subject")]
    public async Task<IActionResult> UpdateSubject([FromBody]AddSubjectDto subjectDto)
    {
        if (ModelState.IsValid)
        {
            var subject = await _subjectService.UpdateSubjectAsync(subjectDto);
            return StatusCode(subject.StatusCode, subject);
        }
        return BadRequest(ModelState);
    }
    
    [HttpDelete("delete/subject")]
    public async Task<IActionResult> DeleteSubject([FromQuery]int id)
    {
        if (ModelState.IsValid)
        {
            var subject = await _subjectService.DeleteSubjectAsync(id);
            return StatusCode(subject.StatusCode, subject);
        }
        return BadRequest(ModelState);
    }
    
    


}