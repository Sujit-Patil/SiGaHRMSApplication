using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;


namespace SiGaHRMS.ApiService.Service
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly IEmployeeSalaryRepository _employeeSalaryRepository;
        private readonly IEmployeeSalaryStructureRepository _employeeSalaryStructureRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAuditingService _auditingService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<EmployeeSalaryService> _logger;

        /// <summary>
        /// Initializes a new instance of the EmployeeSalaryService class.
        /// </summary>
        /// <param name="employeeSalaryRepository">The repository for managing employee salary data.</param>
        /// <param name="logger">The logger for logging messages related to EmployeeSalaryService.</param>
        /// <param name="leaveRequestRepository">The repository for managing leave request data.</param>
        /// <param name="employeeSalaryStructureRepository">The repository for managing employee salary structure data.</param>
        /// <param name="auditingService">The service for auditing operations.</param>
        /// <param name="dateTimeProvider">The provider for date and time operations.</param>
        public EmployeeSalaryService(
            IEmployeeSalaryRepository employeeSalaryRepository,
            ILogger<EmployeeSalaryService> logger,
            ILeaveRequestRepository leaveRequestRepository,
            IEmployeeSalaryStructureRepository employeeSalaryStructureRepository,
            IAuditingService auditingService,
            IDateTimeProvider dateTimeProvider)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
            _employeeSalaryStructureRepository = employeeSalaryStructureRepository;
            _leaveRequestRepository = leaveRequestRepository;
            _auditingService = auditingService;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc/>
        public async Task AddEmployeeSalaryAsync(EmployeeSalary employeeSalary)
        {
            if (!IsCurrentMonth(employeeSalary.SalaryForAMonth))
            {
                return;
            }

            var salaryStructure = await GetEmployeeSalaryStructureAsync(employeeSalary.EmployeeId);
            if (salaryStructure == null)
            {
                throw new ArgumentNullException(nameof(salaryStructure), "Salary structure cannot be null");
            }

            if (await EmployeeSalaryExistsAsync(employeeSalary))
            {
                return;
            }

            SetEmployeeSalaryDetails(employeeSalary, salaryStructure);
            employeeSalary = _auditingService.SetAuditedEntity(employeeSalary, true);

            await _employeeSalaryRepository.AddAsync(employeeSalary);
            await _employeeSalaryRepository.CompleteAsync();

            _logger.LogInformation($"[AddEmployeeSalaryAsync] - {employeeSalary.EmployeeSalaryId} added successfully");
        }

        /// <inheritdoc/>
        public async Task UpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary)
        {
            if (!IsCurrentMonth(employeeSalary.SalaryForAMonth))
            {
                return;
            }

            var salaryStructure = await GetEmployeeSalaryStructureAsync(employeeSalary.EmployeeId);
            if (salaryStructure == null)
            {
                throw new ArgumentNullException(nameof(salaryStructure), "Salary structure cannot be null");
            }

            var existingEmployeeSalary = await GetExistingEmployeeSalaryAsync(employeeSalary.EmployeeId, employeeSalary.SalaryForAMonth);
            if (existingEmployeeSalary == null)
            {
                throw new ArgumentNullException(nameof(existingEmployeeSalary), "Employee salary does not exist");
            }

            SetEmployeeSalaryDetails(employeeSalary, salaryStructure);
            _auditingService.SetAuditedEntity(existingEmployeeSalary, false);

            await _employeeSalaryRepository.UpdateAsync(existingEmployeeSalary);
            await _employeeSalaryRepository.CompleteAsync();

            _logger.LogInformation($"[UpdateEmployeeSalaryAsync] - EmployeeSalary updated successfully for {existingEmployeeSalary.EmployeeSalaryId}");
        }

        /// <inheritdoc/>
        public async Task<EmployeeSalary?> GetEmployeeSalaryByIdAsync(int id)
        {
            return await _employeeSalaryRepository.FirstOrDefaultAsync(x => x.EmployeeSalaryId == id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<EmployeeSalary>> GetAllEmployeeSalariesAsync()
        {
            return await _employeeSalaryRepository.GetAllAsync("Employee");
        }

        /// <inheritdoc/>
        public async Task DeleteEmployeeSalaryAsync(int employeeSalaryId)
        {
            await _employeeSalaryRepository.DeleteAsync(x => x.EmployeeSalaryId == employeeSalaryId);
            await _employeeSalaryRepository.CompleteAsync();

            _logger.LogInformation($"[DeleteEmployeeSalaryAsync] - EmployeeSalary deleted successfully for {employeeSalaryId}");
        }

        /// <inheritdoc/>
        public List<EmployeeSalary> GetEmployeeSalaryByDateAsync(RequestDto employeeSalaryDto)
        {
            var query = _employeeSalaryRepository.GetQueryable(
                filter: x => DateOnly.FromDateTime(x.SalaryForAMonth) >= employeeSalaryDto.FormDate
                          && DateOnly.FromDateTime(x.SalaryForAMonth) <= employeeSalaryDto.ToDate
                          && !x.IsDeleted,
                include: x => x.Include(e => e.Employee));

            if (employeeSalaryDto.EmployeeId.HasValue)
            {
                query = query.Where(x => x.EmployeeId == employeeSalaryDto.EmployeeId);
            }

            return query.ToList();
        }

        #region Private Methods

        private async Task<EmployeeSalaryStructure?> GetEmployeeSalaryStructureAsync(long employeeId)
        {
            return await _employeeSalaryStructureRepository
                .GetQueryable(x => x.EmployeeId == employeeId && x.ToDate == null)
                .FirstOrDefaultAsync();
        }

        private async Task<EmployeeSalary?> GetExistingEmployeeSalaryAsync(long employeeId, DateTime salaryForAMonth)
        {
            return await _employeeSalaryRepository.FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId && x.SalaryForAMonth.Month == salaryForAMonth.Month);
        }

        private bool IsCurrentMonth(DateTime salaryMonth)
        {
            return salaryMonth.Month == _dateTimeProvider.Now.Month;
        }

        private async Task<bool> EmployeeSalaryExistsAsync(EmployeeSalary employeeSalary)
        {
            return await _employeeSalaryRepository
                .GetQueryable(x => x.EmployeeId == employeeSalary.EmployeeId
                                && x.SalaryForAMonth.Month == employeeSalary.SalaryForAMonth.Month)
                .AnyAsync();
        }

        private void SetEmployeeSalaryDetails(EmployeeSalary employeeSalary, EmployeeSalaryStructure structure)
        {
            employeeSalary.DaysInAMonth = DateTime.DaysInMonth(_dateTimeProvider.Now.Year, _dateTimeProvider.Now.Month);
            employeeSalary.PresentDays = employeeSalary.DaysInAMonth - CalculatePresentDays(employeeSalary.EmployeeId, employeeSalary.SalaryForAMonth);
            employeeSalary.Leaves = CalculatePresentDays(employeeSalary.EmployeeId, employeeSalary.SalaryForAMonth);
            employeeSalary.DA = structure.DA / 12;
            employeeSalary.TDS = structure.TDS / 12;
            employeeSalary.HRA = structure.HRA / 12;
            employeeSalary.MedicalAllowance = structure.MedicalAllowance / 12;
            employeeSalary.Basic = structure.Basic / 12;
            employeeSalary.Conveyance = structure.Conveyance / 12;
            employeeSalary.GrossSalary = employeeSalary.DA + employeeSalary.Basic + employeeSalary.HRA + employeeSalary.Conveyance;
            employeeSalary.LeaveDeduction = employeeSalary.GrossSalary - (employeeSalary.GrossSalary / employeeSalary.DaysInAMonth) * employeeSalary.Leaves;
            employeeSalary.NetSalary = employeeSalary.GrossSalary - (employeeSalary.TDS + employeeSalary.PT + employeeSalary.LeaveDeduction);
        }

        private int CalculatePresentDays(long employeeId, DateTime salaryMonth)
        {
            var leaveRequests = _leaveRequestRepository.GetQueryable(x => x.EmployeeId == employeeId
                                                                      && x.FromDate.Month == salaryMonth.Month - 1
                                                                      && x.LeaveRequestStatus == LeaveRequestStatus.Approved)
                                                       .ToList();
            var totalDaysInMonth = DateTime.DaysInMonth(salaryMonth.Year, salaryMonth.Month - 1);

            var leaveDays = leaveRequests.Sum(leave =>
            {
                var endDate = leave.ToDate.Month != leave.FromDate.Month
                              ? _dateTimeProvider.LastDateOfMonth(leave.FromDate)
                              : leave.ToDate;
                var leaveDays = _dateTimeProvider.CalculateDateDifferenceInDays(leave.FromDate, endDate);
                return leave.IsHalfDay == true ? 0.5 : leaveDays;
            });

            return totalDaysInMonth - (int)leaveDays;
        }

        #endregion
    }
}
