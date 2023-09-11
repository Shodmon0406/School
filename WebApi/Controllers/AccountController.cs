using Domain.Dtos.AccountDtos;
using Domain.Responses;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    public async Task<Response<string>> Register([FromBody]RegisterDto model)
    {
        return await _accountService.Register(model);
    }
    [HttpPost("Login")]
    public async Task<Response<string>> Login([FromBody]LoginDto model)
    {
        return await _accountService.Login(model);
    }
}