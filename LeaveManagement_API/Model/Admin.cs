using System.ComponentModel.DataAnnotations;

namespace LeaveManagement_API.Model
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
