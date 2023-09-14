using Domain.Dtos.Account;

using Domain.Dtos.AccountDtos;

using Domain.Responses;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{

    Task<Response<string>> Register(Register model);
    Task<Response<string>> Login(Login model);

    Task<Response<string>> Register(RegisterDto model);
    Task<Response<string>> Login(LoginDto model);

}