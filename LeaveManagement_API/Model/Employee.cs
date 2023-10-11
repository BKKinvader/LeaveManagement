using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
