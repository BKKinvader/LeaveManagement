using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int MaxDays { get; set; }

        // Navigation property för Ledighetsansökningar
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
