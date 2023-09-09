using System.Net;
using Domain.Dtos.Class;
using Domain.Filters.ClassFilter;
using Domain.Responses;
using Infrastructure.Services.ClassService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ClassController :Controller
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpGet("get-all-class")]
    public async Task<IActionResult> GetAllClass([FromQuery]GetClassFilter filter)
    {
        if (ModelState.IsValid)
        {
            var result = await _classService.GetClass(filter);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<List<GetClassDto>>(HttpStatusCode.BadRequest, "Not found");
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("get-all-class-by-id")]
    public async Task<IActionResult> GetClassById([FromQuery]int classId)
    {
        if (ModelState.IsValid)
        {
            var result = await _classService.GetClassById(classId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetClassDto>(HttpStatusCode.BadRequest, "Not found");
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost("add-class")]
    public async Task<IActionResult> AddClass([FromBody]AddClassDto addClass)
    {
        if (ModelState.IsValid)
        {
            var result = await _classService.AddClass(addClass);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetClassDto>(HttpStatusCode.BadRequest, "Not found");
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPost("update-class")]
    public async Task<IActionResult> UpdateClass([FromBody]AddClassDto updateClass)
    {
        if (ModelState.IsValid)
        {
            var result = await _classService.UpdateClass(updateClass);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetClassDto>(HttpStatusCode.BadRequest, "Not found");
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("delete-class")]
    public async Task<IActionResult> DeleteClass([FromQuery]int classId)
    {
        if (ModelState.IsValid)
        {
            var result = await _classService.DeleteClass(classId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<GetClassDto>(HttpStatusCode.BadRequest, "Not found");
        return StatusCode(response.StatusCode, response);
    }
}