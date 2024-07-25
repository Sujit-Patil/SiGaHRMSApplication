using Microsoft.EntityFrameworkCore;
using SiGaHRMS.ApiService.Interfaces;
using SiGaHRMS.Data.Interfaces;
using SiGaHRMS.Data.Model;
using SiGaHRMS.Data.Model.Dto;
using SiGaHRMS.Data.Model.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGaHRMS.ApiService.Service
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IAuditingService _auditingService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<LeaveRequestService> _logger;

        public LeaveRequestService(
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveBalanceRepository leaveBalanceRepository,
            ILeaveBalanceService leaveBalanceService,
            IAuditingService auditingService,
            IDateTimeProvider dateTimeProvider,
            ILogger<LeaveRequestService> logger,
            IHolidayRepository holidayRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveBalanceRepository = leaveBalanceRepository;
            _leaveBalanceService = leaveBalanceService;
            _auditingService = auditingService;
            _dateTimeProvider = dateTimeProvider;
            _holidayRepository = holidayRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);

            if (leaveBalance == null) throw new ArgumentNullException(nameof(leaveBalance));

            if (await IsLeaveRequestConflict(leaveRequest, LeaveRequestStatus.Open)) return;

            SetLeaveTypeIfNotApplicable(leaveRequest, leaveBalance);

            await SaveLeaveRequestAsync(leaveRequest, true);
        }


        /// <inheritdoc/>
        public async Task UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var existingLeaveRequest = await _leaveRequestRepository
                .FirstOrDefaultAsync(x => x.LeaveRequestId == leaveRequest.LeaveRequestId);
            var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);

            if (existingLeaveRequest == null || leaveBalance == null || await IsLeaveRequestConflict(leaveRequest, LeaveRequestStatus.Approved))
                return;

            SetLeaveTypeIfNotApplicable(existingLeaveRequest, leaveBalance);

            if (existingLeaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
            {
                await HandleLeaveRequestStatusChange(existingLeaveRequest, leaveBalance, leaveRequest.IsDeleted);
            }

            UpdateLeaveRequestDetails(existingLeaveRequest, leaveRequest);
            await SaveLeaveRequestAsync(existingLeaveRequest, false);
        }

        /// <inheritdoc/>
        public async Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest)
        {
            if (leaveRequest.LeaveRequestStatus == LeaveRequestStatus.Approved)
            {
                var leaveBalance = await _leaveBalanceService.GetLeaveBalanceByIdAsync(leaveRequest.EmployeeId);
                if (leaveBalance != null)
                {
                    var days = _dateTimeProvider.CalculateWorkingDateDifferenceInDays(leaveRequest.FromDate, leaveRequest.ToDate);
                    days -= CountWeekdayHolidays(leaveRequest.FromDate, leaveRequest.ToDate);
                    await AdjustLeaveBalanceAsync(leaveBalance, leaveRequest.LeaveType, (short)days, false);
                }
            }

            await SaveLeaveRequestAsync(leaveRequest, false);
        }

        /// <inheritdoc/>
        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
        {
            return await _leaveRequestRepository.FirstOrDefaultAsync(x => x.LeaveRequestId == id);
        }

        /// <inheritdoc/>
        public List<LeaveRequest> GetAllLeaveRequests()
        {
            return _leaveRequestRepository
                .GetQueryable(x => !x.IsDeleted, y => y.Include(x => x.Employee))
                .ToList();
        }

        /// <inheritdoc/>
        public async Task DeleteLeaveRequestAsync(int leaveRequestId)
        {
            await _leaveRequestRepository.DeleteAsync(x => x.LeaveRequestId == leaveRequestId);
            await _leaveRequestRepository.CompleteAsync();
            _logger.LogInformation($"[DeleteLeaveRequestAsync] - LeaveRequest {leaveRequestId} deleted successfully");
        }

        /// <inheritdoc/>
        public List<LeaveRequest> GetLeaveRequestsByDateAsync(RequestDto leaveRequestDto)
        {
            var query = _leaveRequestRepository.GetQueryable(x => !x.IsDeleted, y => y.Include(x => x.Employee));

            if (leaveRequestDto?.EmployeeId != null)
                query = query.Where(x => x.EmployeeId == leaveRequestDto.EmployeeId);

            if (leaveRequestDto?.FormDate != null)
                query = query.Where(x => x.FromDate >= leaveRequestDto.FormDate);

            if (leaveRequestDto?.ToDate != null)
                query = query.Where(x => x.ToDate <= leaveRequestDto.ToDate);
            else
                query = query.Where(x => x.ToDate >= DateOnly.FromDateTime(DateTime.Today));

            return query.ToList();
        }

        #region Private Methods

        private void SetLeaveTypeIfNotApplicable(LeaveRequest leaveRequest, LeaveBalance leaveBalance)
        {
            if (leaveBalance.LeaveBalanceStatus == LeaveBalanceStatus.NotApplicable)
                leaveRequest.LeaveType = LeaveType.LossOfPay;
        }

        private async Task<bool> IsLeaveRequestConflict(LeaveRequest leaveRequest, LeaveRequestStatus status)
        {
            var conflict = await _leaveRequestRepository
                .GetQueryable(x =>
                    !x.IsDeleted &&
                    x.LeaveRequestStatus == status &&
                    x.EmployeeId == leaveRequest.EmployeeId &&
                    x.FromDate <= leaveRequest.ToDate &&
                    x.ToDate >= leaveRequest.FromDate)
                .FirstOrDefaultAsync();

            return conflict != null;
        }

        private async Task SaveLeaveRequestAsync(LeaveRequest leaveRequest, bool isNew)
        {
            leaveRequest = _auditingService.SetAuditedEntity(leaveRequest, created: isNew);

            if (isNew)
                await _leaveRequestRepository.AddAsync(leaveRequest);
            else
                await _leaveRequestRepository.UpdateAsync(leaveRequest);

            await _leaveRequestRepository.CompleteAsync();
            _logger.LogInformation($"[SaveLeaveRequestAsync] - LeaveRequest {leaveRequest.LeaveRequestId} {(isNew ? "added" : "updated")} successfully");
        }

        private async Task HandleLeaveRequestStatusChange(LeaveRequest existingLeaveRequest, LeaveBalance leaveBalance, bool isDeleted)
        {
            var previousDays = _dateTimeProvider.CalculateWorkingDateDifferenceInDays(existingLeaveRequest.FromDate, existingLeaveRequest.ToDate);
            previousDays -= CountWeekdayHolidays(existingLeaveRequest.FromDate, existingLeaveRequest.ToDate);
            await AdjustLeaveBalanceAsync(leaveBalance, existingLeaveRequest.LeaveType, previousDays, true);
        }

        private short CountWeekdayHolidays(DateOnly fromDate, DateOnly toDate)
        {
            return (short)_holidayRepository
                .GetQueryable(x => !x.IsDeleted && x.Date >= fromDate && x.Date <= toDate)
                .Count(x => x.Date.DayOfWeek != DayOfWeek.Saturday && x.Date.DayOfWeek != DayOfWeek.Sunday);
        }

        private void UpdateLeaveRequestDetails(LeaveRequest existingLeaveRequest, LeaveRequest leaveRequest)
        {
            existingLeaveRequest.LeaveRequestStatus = leaveRequest.IsDeleted ? leaveRequest.LeaveRequestStatus : LeaveRequestStatus.Open;
            existingLeaveRequest.IsDeleted = leaveRequest.IsDeleted;
            existingLeaveRequest.FromDate = leaveRequest.FromDate;
            existingLeaveRequest.ToDate = leaveRequest.ToDate;
            existingLeaveRequest.LeaveType = leaveRequest.LeaveType;
        }

        private async Task AdjustLeaveBalanceAsync(LeaveBalance leaveBalance, LeaveType leaveType, short days, bool isRollback)
        {
            var adjustment = (short)(isRollback ? -days : days);
            switch (leaveType)
            {
                case LeaveType.SickLeave:
                    leaveBalance.SickLeavesAvailaed += adjustment;
                    break;
                case LeaveType.CasualLeave:
                    leaveBalance.CasualLeavesAvailaed += adjustment;
                    break;
                case LeaveType.MaternityLeave:
                    leaveBalance.MaternityLeavesAvailaed += adjustment;
                    break;
                case LeaveType.BereavementLeave:
                    leaveBalance.BereavementLeavesAvailaed += adjustment;
                    break;
                case LeaveType.PaternityLeave:
                    leaveBalance.PaternityLeavesAvailaed += adjustment;
                    break;
                case LeaveType.CompensatoryOff:
                    leaveBalance.CompensatoryOffsAvailaed += adjustment;
                    break;
                case LeaveType.EarnedLeave:
                    leaveBalance.EarnedLeavesAvailaed += adjustment;
                    break;
                case LeaveType.MarriageLeave:
                    leaveBalance.MarriageLeavesAvailaed += adjustment;
                    break;
                default:
                    leaveBalance.LossofPayLeavesAvailaed += adjustment;
                    break;
            }

            await _leaveBalanceService.UpdateLeaveBalanceAsync(leaveBalance);
        }

        #endregion
    }
}
