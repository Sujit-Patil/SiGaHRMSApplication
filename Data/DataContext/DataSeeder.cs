using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGaHRMS.Data.DataContext;


public static class DataSeeder
{
    private static readonly string[] roles = new string[]
    {
    "Super Admin",
    "Management",
    "HR",
    "Sales & Marketing",
    "Accounts & Finance",
    "Manager",
    "QA",
    "Developer"
    };

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Seed Roles
            await SeedRolesAsync(roleManager);

            // Seed Default Users
            await SeedDefaultUsersAsync(userManager);

            // Seed Designations
            if (!context.Designations.Any())
            {
                context.Designations.AddRange(
                    new Designation { DesignationName = "Intern" },
                    new Designation { DesignationName = "Junior Software Developer" },
                    new Designation { DesignationName = "Software Developer" },
                    new Designation { DesignationName = "Senior Software Developer" },
                    new Designation { DesignationName = "Junior Graphic Designer" },
                    new Designation { DesignationName = "Graphic Designer" },
                    new Designation { DesignationName = "Senior Graphic Designer" },
                    new Designation { DesignationName = "Junior Web Designer" },
                    new Designation { DesignationName = "Web Designer" },
                    new Designation { DesignationName = "Senior Web Designer" },
                    new Designation { DesignationName = "Junior QA" },
                    new Designation { DesignationName = "QA" },
                    new Designation { DesignationName = "Senior QA" },
                    new Designation { DesignationName = "Team Lead" },
                    new Designation { DesignationName = "Project Manager" },
                    new Designation { DesignationName = "Junior HR" },
                    new Designation { DesignationName = "HR" },
                    new Designation { DesignationName = "Senior HR" },
                    new Designation { DesignationName = "Junior Marketing Analyst" },
                    new Designation { DesignationName = "Marketing Analyst" },
                    new Designation { DesignationName = "Senior Marketing Analyst" },
                    new Designation { DesignationName = "COO" },
                    new Designation { DesignationName = "CTO" },
                    new Designation { DesignationName = "CEO" },
                    new Designation { DesignationName = "Director" }
                );
                context.SaveChanges();
            }

            // Seed Billing Platforms
            if (!context.BillingPlatforms.Any())
            {
                context.BillingPlatforms.AddRange(
                    new BillingPlatform { Name = "Upwork" },
                    new BillingPlatform { Name = "Payoneer" },
                    new BillingPlatform { Name = "Bank Transfer" }
                );
                context.SaveChanges();
            }

            // Seed Holidays
            if (!context.Holidays.Any())
            {
                context.Holidays.AddRange(
                    new Holiday { Date = new DateOnly(2024, 8, 1), Description = "Trip to Goa" },
                    new Holiday { Date = new DateOnly(2024, 1, 26), Description = "Republic Day" },
                    new Holiday { Date = new DateOnly(2024, 3, 25), Description = "Holi" },
                    new Holiday { Date = new DateOnly(2024, 4, 9), Description = "Gudhi Padwa" },
                    new Holiday { Date = new DateOnly(2024, 5, 1), Description = "Maharashtra Din / Kamgar Din" },
                    new Holiday { Date = new DateOnly(2024, 8, 15), Description = "Independence Day" },
                    new Holiday { Date = new DateOnly(2024, 10, 2), Description = "Gandhi Jayanti" },
                    new Holiday { Date = new DateOnly(2024, 10, 31), Description = "Diwali" },
                    new Holiday { Date = new DateOnly(2024, 11, 1), Description = "Diwali" },
                    new Holiday { Date = new DateOnly(2024, 12, 25), Description = "Christmas" }
                );
                context.SaveChanges();
            }

            // Seed Incentive Purposes
            if (!context.IncentivePurposes.Any())
            {
                context.IncentivePurposes.AddRange(
                    new IncentivePurpose { Purpose = "Performance Bonus" },
                    new IncentivePurpose { Purpose = "Work Anniversary" },
                    new IncentivePurpose { Purpose = "Client Appreciation" },
                    new IncentivePurpose { Purpose = "Extra Ordinary Achievement" },
                    new IncentivePurpose { Purpose = "Special Recognition" },
                    new IncentivePurpose { Purpose = "Diwali Gift" },
                    new IncentivePurpose { Purpose = "Birthday Gift" }
                );
                context.SaveChanges();
            }

            // Seed Leave Masters
            if (!context.LeaveMasters.Any())
            {
                context.LeaveMasters.AddRange(
                    new LeaveMaster { LeaveType = "Earned Leave (EL)", LeaveCount = 0 },
                    new LeaveMaster { LeaveType = "Casual Leave (CL)", LeaveCount = 12 },
                    new LeaveMaster { LeaveType = "Sick Leave (SL)", LeaveCount = 0 },
                    new LeaveMaster { LeaveType = "Maternity Leave (ML)", LeaveCount = 0 },
                    new LeaveMaster { LeaveType = "Compensatory Off (Comp-off)", LeaveCount = 0 },
                    new LeaveMaster { LeaveType = "Marriage Leave", LeaveCount = 5 },
                    new LeaveMaster { LeaveType = "Paternity Leave", LeaveCount = 0 },
                    new LeaveMaster { LeaveType = "Bereavement Leave", LeaveCount = 3 },
                    new LeaveMaster { LeaveType = "Loss of Pay (LOP)", LeaveCount = 0 }
                );
                context.SaveChanges();
            }

            // Seed Employees
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                   new Employee
                   {
                       FirstName = "Test1",
                       LastName = "Test1",
                       Gender = "Test1",
                       DateOfBirth = new DateOnly(1985, 10, 15),
                       ContactNumber = "1234567890",
                       PersonalEmail = "Test1@example.com",
                       CompanyEmail = "Test1@company.com",
                       DateOfJoining = new DateOnly(2020, 1, 1),
                       CurrentDesignation = "Software Developer",
                       CurrentGrossSalary = 80000,
                       EmployeeStatus = EmployeeStatus.Active

                   },
                   new Employee
                   {
                       FirstName = "Test2",
                       LastName = "Test2",
                       Gender = "Test2",
                       DateOfBirth = new DateOnly(1990, 5, 20),
                       ContactNumber = "9876543210",
                       PersonalEmail = "Test2@example.com",
                       CompanyEmail = "Test2@company.com",
                       DateOfJoining = new DateOnly(2018, 7, 15),
                       CurrentDesignation = "HR Manager",
                       CurrentGrossSalary = 100000,
                       EmployeeStatus = EmployeeStatus.Active

                   }
                );
                context.SaveChanges();
            }

            // Seed Departments
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { DepartmentName = "Management" },
                    new Department { DepartmentName = "HR" },
                    new Department { DepartmentName = "Sales & Marketing" },
                    new Department { DepartmentName = "Accounts & Finance" },
                    new Department { DepartmentName = "Development" }
                );
                context.SaveChanges();
            }

            // Seed Departments
            if (!context.Clients.Any())
            {
                context.Clients.AddRange(
                   new Client
                   {
                       Name = "John Doe",
                       CompanyName = "Doe Enterprises",
                       ContactPersonName = "John Doe",
                       Status = ClientStatus.Active,
                       CreatedDateTime = DateTime.Now
                   },
                new Client
                {
                    Name = "Jane Smith",
                    CompanyName = "Smith LLC",
                    ContactPersonName = "Jane Smith",
                    Status = ClientStatus.Active,
                    CreatedDateTime = DateTime.Now
                },
                new Client
                {
                    Name = "Bob Johnson",
                    CompanyName = "Johnson Corp",
                    ContactPersonName = "Bob Johnson",
                    Status = ClientStatus.Inactive,
                    CreatedDateTime = DateTime.Now
                }
                );
                context.SaveChanges();
            }
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedDefaultUsersAsync(UserManager<IdentityUser> userManager)
    {
        var defaultUser = new IdentityUser { UserName = "sahadev2612@gmail.com", Email = "sahadev2612@gmail.com", EmailConfirmed = true };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Sada@2612");
                await userManager.AddToRoleAsync(defaultUser, "Super Admin");
            }
        }
    }
}
