using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int MaxDays { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
