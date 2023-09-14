using Domain.Dtos.StipendDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Services.StipendService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace WebApi.Controllers
{
    public class StipendController : BaseController
    {
        private readonly IStipendService _service;

        public StipendController(IStipendService service)
        {
            _service = service;
        }

        [HttpGet("get-stipends")]
        public Task<Response<List<GetStipendDto>>> GetStipendsAsync()
        {
            return _service.GetStipendsAsync();
        }

        [HttpGet("get-stipend-by-id")]
        public Task<Response<GetStipendDto>> GetStipendAsync(int id)
        {
            return _service.GetStipendAsync(id);
        }

        [HttpPost("create-stipends")]
        public async Task<IActionResult> CreateStipendAsync(AddStipendDto stipend)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateStipendAsync(stipend);
                return StatusCode(result.StatusCode, result);
            }

            var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErors());
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update-stipend")]
        public async Task<IActionResult> UpdateStipendAsync(AddStipendDto stipend)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateStipendAsync(stipend);
                return StatusCode(result.StatusCode, result);
            }

            var response = new Response<int>(HttpStatusCode.BadRequest, ModelStateErors());
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete-stipend")]
        public async Task<IActionResult> DeleteStipendAsync(int id)
        {
            var response = await _service.DeleteStipendAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
