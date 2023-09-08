using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        

        services.AddIdentity<IdentityUser, IdentityRole>(config =>
        {
            config.Password.RequiredLength = 4;
            config.Password.RequireDigit = false; // must have at least one digit
            config.Password.RequireNonAlphanumeric = false; // must have at least one non-alphanumeric character
            config.Password.RequireUppercase = false; // must have at least one uppercase character
            config.Password.RequireLowercase = false;  // must have at least one lowercase character
        })
            //for registering usermanager and signinmanger
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
    }
}