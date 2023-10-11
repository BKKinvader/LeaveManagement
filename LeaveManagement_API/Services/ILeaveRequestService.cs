using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface ILeaveRequestService
    {
        public Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests();
        public Task<LeaveRequest> GetLeaveRequestById(int id);
        public Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequest);
        public Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest leaveRequest);
        public Task<LeaveRequest> DeleteLeaveRequest(int id);

        public Task SaveAsync();
    }
}
