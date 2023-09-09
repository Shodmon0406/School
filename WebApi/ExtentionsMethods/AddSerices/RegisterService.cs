using Infrastructure.Data;
using Infrastructure.Services.Claas;
using Infrastructure.Services.ClassroomService;
using Infrastructure.Services.ClassService;
using Infrastructure.Services.StudentServices;
using Infrastructure.Services.SubjectService;
using Infrastructure.Services.TeacherServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClassService, ClassService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddScoped<ISubjectService, SubjectService>();

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