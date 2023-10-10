using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        public string Comments { get; set; }

        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }

        public enum LeaveStatus
        {
            Pending,
            Approved,
            Rejected
        }
    }
}
