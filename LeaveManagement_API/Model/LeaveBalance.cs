namespace LeaveManagement_API.Model
{
    public class LeaveBalance
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int RemainingDays { get; set; }

        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
