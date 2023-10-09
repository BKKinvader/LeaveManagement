using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public ICollection<LeaveRequest> LeaveTables { get; set; }
    }
}
