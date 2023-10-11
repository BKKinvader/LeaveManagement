using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement_API.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly AppDbContext _dbContext;

        public LeaveTypeService(AppDbContext dbcontext)
        {
            this._dbContext = dbcontext;

        }


        public async Task<LeaveType> AddLeaveType(LeaveType leaveType)
        {
            await _dbContext.LeaveTypes.AddAsync(leaveType);
            await _dbContext.SaveChangesAsync();
            return leaveType;
        }

        public async Task<LeaveType> DeleteLeaveType(int id)
        {
            
            
                var leavetype = await _dbContext.LeaveTypes.FindAsync(id);
                _dbContext.LeaveTypes.Remove(leavetype);
                await _dbContext.SaveChangesAsync();
                return leavetype;
            
            
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveTypes()
        {
            if(_dbContext.LeaveTypes == null)
            {
                Console.WriteLine("No LeaveTypes found");
                return null;
            }
            return await _dbContext.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeById(int id)
        {
            var leavetype = await _dbContext.LeaveTypes.FindAsync(id);
            return leavetype;
        }

        public async Task<LeaveType> GetLeaveTypeByName(string TypeName)
        {
            var leaveType = await _dbContext.LeaveTypes.FirstOrDefaultAsync(lt => lt.TypeName == TypeName);

            return leaveType;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LeaveType> UpdateLeaveType(LeaveType leaveType)
        {
            var LeaveTypeToUpDate = await _dbContext.LeaveTypes.FirstOrDefaultAsync(lt => lt.Id == leaveType.Id);
            if(LeaveTypeToUpDate != null)
            {
                LeaveTypeToUpDate.TypeName = leaveType.TypeName;
                LeaveTypeToUpDate.MaxDays = leaveType.MaxDays;
            }

            await _dbContext.SaveChangesAsync();
            return LeaveTypeToUpDate;
        }
    }
}
