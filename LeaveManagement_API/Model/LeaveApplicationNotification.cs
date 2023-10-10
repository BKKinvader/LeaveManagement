namespace LeaveManagement_API.Model
{
    public class LeaveApplicationNotification
    {
        public Guid Id { get; set; }
        public Guid LeaveRequestId { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotificationDate { get; set; }

        public LeaveRequest LeaveRequest { get; set; }
    }
}
