using Domain.Dtos;
using Domain.Filters.ParentFilter;
using Domain.Responses;
using Infrastructure.Services.ParentService.cs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParentController : ControllerBase
{
    private readonly IParentService _parentService;

    public ParentController(IParentService parentService)
    {
        _parentService = parentService;
    }
    [HttpGet("GetAllParents")]
    async public Task<Response<List<GetParentDto>>> GetAllParents(GetParentFilter filter)
    {
        return await _parentService.GetAllParents(filter);
    }
    [HttpGet("GetParentById")]
    async public Task<Response<GetParentDto>> GetParentById(int id)
    {
        return await _parentService.GetParentById(id);
    }

    [HttpPost("AddParent")]
    async public Task<Response<GetParentDto>> AddParent([FromQuery]AddParentDto parent)
    {
        return await _parentService.AddParent(parent);
    }

    [HttpPut("UpdateParent")]
    async public Task<Response<GetParentDto>> UpdateParent(AddParentDto parent)
    {
        return await _parentService.UpdateParent(parent);
    }

    [HttpDelete("DeleteParent")]
    async public Task<Response<bool>> DeleteParent(int id)
    {
        return await _parentService.DeleteParent(id);
    }
}
