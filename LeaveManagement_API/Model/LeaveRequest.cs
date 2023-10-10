using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class LeaveRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; } // Statusen för ledighetsansökan (Pending, Approved, Rejected)

        // Navigation properties
        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }

        public enum LeaveStatus
        {
            Pending,  // Ledighetsansökan är väntande
            Approved, // Ledighetsansökan har godkänts
            Rejected  // Ledighetsansökan har avslagits
        }
    }
}
