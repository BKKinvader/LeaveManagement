using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public LeaveStatus Status { get; set; }
        [MaxLength(255)]
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
