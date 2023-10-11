using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface ILeaveRequestService
    {
        public Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests();
        public Task<LeaveRequest> GetLeaveRequestById(Guid id);
        public Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequest);
        public Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest leaveRequest);
        public Task<LeaveRequest> DeleteLeaveRequest(Guid id);

        public Task SaveAsync();
    }
}
