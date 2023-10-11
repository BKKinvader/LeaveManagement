namespace LeaveManagement_API.Model.DTOs
{
    public class LeaveBalanceDTO
    { 
        public Guid EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int RemainingDays { get; set; }

    }
}
