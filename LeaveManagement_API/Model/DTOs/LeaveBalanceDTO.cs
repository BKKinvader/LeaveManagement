namespace LeaveManagement_API.Model.DTOs
{
    public class LeaveBalanceDTO
    { 
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int RemainingDays { get; set; }

    }
}
