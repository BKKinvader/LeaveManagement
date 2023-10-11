using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement_API.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly AppDbContext _dbContext;

        public LeaveRequestService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            await _dbContext.LeaveRequests.AddAsync(leaveRequest);
            await _dbContext.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<LeaveRequest> DeleteLeaveRequest(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequests.FindAsync(id);
            _dbContext.LeaveRequests.Remove(leaveRequest);
            await _dbContext.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests()
        {
            if (_dbContext == null)
            {
                Console.WriteLine("No Leave requests is available at this moment");
                return null;
            }
            return await _dbContext.LeaveRequests.ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestById(int id)
        {
            return await _dbContext.LeaveRequests.FindAsync(id);
        }

        public async Task SaveAsync()
        {         
            await _dbContext.SaveChangesAsync();        
        }

        public async Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _dbContext.LeaveRequests.Update(leaveRequest);
            await _dbContext.SaveChangesAsync();
            return leaveRequest;
        }
    }
}
