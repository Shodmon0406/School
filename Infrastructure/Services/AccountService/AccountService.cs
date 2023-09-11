using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Dtos.AccountDtos;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountService(UserManager<IdentityUser> userManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<Response<string>> Register(RegisterDto model)
    {
        try
        {
            var student = await _userManager.Users.FirstOrDefaultAsync(e => e.UserName == model.UserName);
            if (student == null)
            {
                var user = new Student()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber

                };
             await _userManager.CreateAsync(user, model.Password);
             return new Response<string>($"Registered successful {user.Id}");
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, "UserName already registered");
            }
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> Login(LoginDto model)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
              var password = await _userManager.CheckPasswordAsync(user, model.Password);
              if (password)
              {
                  return new Response<string>(GenerateJWTToken(user));
              }
              else
              {
                  return new Response<string>(HttpStatusCode.BadRequest, "UserName or password incorrect");
              }
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Username or password incorrect");
            }
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
  public  string GenerateJWTToken(IdentityUser userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
