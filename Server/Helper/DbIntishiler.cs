using Microsoft.AspNetCore.Identity;

namespace SiGaHRMS.ApiService.Helper;

public static class IdentityDataSeeder
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Seed roles
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Seed users
        if (userManager.Users.Any()) return; // Users already seeded

        var adminUser = new IdentityUser
        {
            UserName = "admin@example.com",
            Email = "admin@example.com"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var regularUser = new IdentityUser
        {
            UserName = "user@example.com",
            Email = "user@example.com"
        };
        result = await userManager.CreateAsync(regularUser, "User@123");

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(regularUser, "User");
        }
    }
}