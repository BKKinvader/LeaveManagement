using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface ILeaveBalanceService
    {
        public Task<IEnumerable<LeaveBalance>> GetAllLeaveBalance();
        public Task<LeaveBalance> GetLeaveBalanceById(int id);
        public Task<LeaveBalance> CreateLeaveBalance(LeaveBalance leaveBalance);
        public Task<LeaveBalance> UpdateLeaveBalance(LeaveBalance leaveBalance);
        public Task<LeaveBalance> DeleteLeaveBalance(int id);

        public Task SaveAsync();
    }
}
