using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public Seeder(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRole()
    {
        var role = new List<IdentityRole>()
        {
            new IdentityRole(Roles.Admin),
            new IdentityRole(Roles.Student),
            new IdentityRole(Roles.Teacher),
            new IdentityRole(Roles.SuperAdmin),
            new IdentityRole(Roles.Viewer)
        };
        var exist = _roleManager.Roles.ToList();
        foreach (var r in role)
        {
            if (exist.Exists(e=>e.Name == r.Name) == false)
            {
               await _roleManager.CreateAsync(r);
            }
        }
    }

    public async Task SeedUser()
    {
        var existing = _userManager.FindByNameAsync(Roles.Admin);
        if (existing == null)
        {
            var admin = new Student()
            {
                UserName = Roles.Admin,
                Email = "fayz00998811@gmail.com",
                PhoneNumber = "+992800151541" 
            };
             await _userManager.CreateAsync(admin);
            await _userManager.AddToRoleAsync(admin,Roles.Admin);
        }
        
    }
}

public class Roles
{
    public const string Admin = "Admin";
    public const string Student = "Student";
    public const string Teacher = "Teacher";
    public const string SuperAdmin = "SuperAdmin";
    public const string Viewer = "Viewer";
}