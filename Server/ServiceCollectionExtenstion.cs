using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.ApiService.Service;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Repository;

namespace SiGaHRMS.HRMS.ApiService;

public static class ServiceCollectionExtenstion
{
    public static void AddService(IServiceCollection Services)
    {
        #region Services

        Services.AddScoped<IAttendanceService, AttendanceService > ();
        Services.AddScoped<IAuthService, AuthService>();
        Services.AddScoped<IBillingPlatformService, BillingPlatformService>();
        Services.AddScoped<IClientService, ClientService>();
        Services.AddScoped<IDepartmentService, DepartmentService>();
        Services.AddScoped<IDesignationService, DesignationService>();
        Services.AddScoped<IEmployeeDesignationService, EmployeeDesignationService>();
        Services.AddScoped<IEmployeeService, EmployeeService>();
        Services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();
        Services.AddScoped<IEmployeeSalaryStructureService, EmployeeSalaryStructureService>();
        Services.AddScoped<IHolidayService, HolidayService>();
        Services.AddScoped<IImageService, ImageService>();
        Services.AddScoped<IIncentivePurposeService, IncentivePurposeService>();
        Services.AddScoped<IIncentiveService, IncentiveService>();
        Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        Services.AddScoped<ILeaveBalanceService, LeaveBalanceService>();
        Services.AddScoped<ILeaveMasterService, LeaveMasterService>();
        Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        Services.AddScoped<IProjectService, ProjectService>();
        Services.AddScoped<ITimeSheetDetailService, TimeSheetDetailService>(); 
        Services.AddScoped<ITimesheetService, TimesheetService>();

        #endregion

        #region Repositories
        Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        Services.AddScoped<IBillingPlatformRepository, BillingPlatformRepository>();
        Services.AddScoped<IClientRepository, ClientRepository>();
        Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        Services.AddScoped<IDesignationRepository, DesignationRepository>();
        Services.AddScoped<IEmployeeDesignationRepository, EmployeeDesignationRepository>();
        Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        Services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();
        Services.AddScoped<IEmployeeSalaryStructureRepository, EmployeeSalaryStructureRepository>();
        Services.AddScoped<IHolidayRepository, HolidayRepository>();
        Services.AddScoped<IIncentivePurposeRepository, IncentivePurposeRepository>();
        Services.AddScoped<IIncentiveRepository, IncentiveRepository>();
        Services.AddScoped<ILeaveBalanceRepository, LeaveBalanceRepository>();
        Services.AddScoped<ILeaveMasterRepository, LeaveMasterRepository>();
        Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        Services.AddScoped<IProjectRepository, ProjectRepository>();
        Services.AddScoped<ITimeSheetDetailRepository, TimeSheetDetailRepository>();
        Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
        #endregion
    }
}
