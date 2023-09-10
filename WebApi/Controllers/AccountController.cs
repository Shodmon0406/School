using Domain.Dtos.Account;
using Domain.Responses;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Identity;
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
    public async Task<Response<string>> Register(Register model)
    {
        return await _accountService.Register(model);
    }

    [HttpPost("Login")]
    public async Task<Response<string>> Login(Login model)
    {
        return await _accountService.Login(model);
    }
}