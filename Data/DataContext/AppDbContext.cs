using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiGaHRMS.Data.Model;

namespace SiGaHRMS.Data.DataContext;

public class AppDbContext : IdentityDbContext
{
    /// <summary>
    /// Initializes a new instance of see ref<paramref name="AppDbContext"/>
    /// </summary>
    /// <param name="DbContextOptions<AppDbContext>"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeDesignation> EmployeeDesignations { get; set; }
    public DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
    public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }
    public DbSet<TimeSheetDetail> TimeSheetDetails { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveBalance> LeaveBalances { get; set; }
    public DbSet<Incentive> Incentives { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<LeaveMaster> LeaveMasters { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<BillingPlatform> BillingPlatforms { get; set; }
    public DbSet<IncentivePurpose> IncentivePurposes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Timesheet>()
        .HasOne(lr => lr.ApproverEmployee)
        .WithMany()
        .HasForeignKey(lr => lr.Approver)
        .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Employee>()
            .HasOne(e => e.TeamLead)
            .WithMany()
            .HasForeignKey(e => e.TeamLeadId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.ReportingManager)
            .WithMany()
            .HasForeignKey(e => e.ReportingManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeStatus)
                .HasConversion<string>();

        modelBuilder.Entity<Project>()
            .Property(p => p.BillingType)
            .HasConversion<string>();

        modelBuilder.Entity<Client>()
            .Property(c => c.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Timesheet>()
            .Property(t => t.TimesheetStatus)
            .HasConversion<string>();

        modelBuilder.Entity<TimeSheetDetail>()
            .Property(td => td.TaskType)
            .HasConversion<string>();

        modelBuilder.Entity<LeaveRequest>()
            .Property(lr => lr.LeaveRequestStatus)
            .HasConversion<string>();

        modelBuilder.Entity<LeaveBalance>()
            .Property(lb => lb.LeaveBalanceStatus)
            .HasConversion<string>();
    }
}
