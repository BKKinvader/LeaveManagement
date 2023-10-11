using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement_API.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly AppDbContext _dbContext;

        public LeaveBalanceService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<LeaveBalance> CreateLeaveBalance(LeaveBalance leaveBalance)
        {
            await _dbContext.LeaveBalances.AddAsync(leaveBalance);
            await _dbContext.SaveChangesAsync();
            return leaveBalance;
        }

        public async Task<LeaveBalance> DeleteLeaveBalance(int id)
        {
            var leaveBalance = await _dbContext.LeaveBalances.FindAsync(id);
            _dbContext.LeaveBalances.Remove(leaveBalance);
            await _dbContext.SaveChangesAsync();
            return leaveBalance;
        }

        public async Task<IEnumerable<LeaveBalance>> GetAllLeaveBalance()
        {
            if (_dbContext == null)
            {
                Console.WriteLine("No Leave Balance is available at this moment");
                return null;
            }
            return await _dbContext.LeaveBalances.ToListAsync();
        }

        public async Task<LeaveBalance> GetLeaveBalanceById(int id)
        {
            return await _dbContext.LeaveBalances.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LeaveBalance> UpdateLeaveBalance(LeaveBalance leaveBalance)
        {
            _dbContext.LeaveBalances.Update(leaveBalance);
            await _dbContext.SaveChangesAsync();
            return leaveBalance;
        }
    }
}
