using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Dtos.Account;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AccountService(UserManager<IdentityUser> userManager,DataContext context,IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }
    public async Task<Response<string>> Register([FromBody]Register model)
    {
        try
        {
            var exist = await _userManager.FindByNameAsync(model.UserName);
            if (exist != null) return new Response<string>(HttpStatusCode.BadRequest, "Such User already exist");
           var user = new Student()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone
            };

           var parent = new Parent()
           {
                FirstName = string.Empty,
                CreatedAt = DateTime.UtcNow,
                Email = string.Empty,
                Phone = string.Empty,
                LastName = string.Empty,
              
                
           };
           await _context.Parents.AddAsync(parent);
            await _userManager.CreateAsync(user,model.Password);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, $"Register successful {user.Id}");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }

    public async Task<Response<string>> Login([FromBody]Login model)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var check = await _userManager.CheckPasswordAsync(user,model.Password);
                if (check)
                {
                    return new Response<string>(GenerateJwtToken(user));
                }
                else
                {
                    return new Response<string>(HttpStatusCode.BadRequest,"UserName or Password was incorrect");
                }
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, "UserName or Password not found");
            }
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,e.Message);
        }
    }
    private string GenerateJwtToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserName) }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

