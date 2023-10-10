using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Employee> ManagedEmployees { get; set; }
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
