using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface ILeaveTypeService
    {
        public Task<IEnumerable<LeaveType>> GetAllLeaveTypes();
        public Task<LeaveType> GetLeaveTypeById(int id);
        public Task<LeaveType> GetLeaveTypeByName(string TypeName);
        public Task<LeaveType> AddLeaveType(LeaveType leaveType);
        public Task<LeaveType> UpdateLeaveType(LeaveType leaveType);
        public Task<LeaveType> DeleteLeaveType(int id);

        public Task SaveAsync();
    }
}
