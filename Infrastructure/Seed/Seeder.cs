using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public Seeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
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