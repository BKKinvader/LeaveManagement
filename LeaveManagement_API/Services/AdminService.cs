using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement_API.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _dbContext;

        public AdminService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Admin>> GetAllAdmin()
        {
            if (_dbContext == null)
            {
                Console.WriteLine("No Admin found");
                return null;
            }
            return await _dbContext.Admins.ToListAsync();
        }


        public async Task<Admin> GetAdminById(int id)
        {
            var admin = await _dbContext.Admins.FindAsync(id);
            return admin;
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return admin;
        }

      

        public async Task<Admin> DeleteAdmin(int id)
        {
            try
            {
                var admin = await _dbContext.Admins.FindAsync(id);
                _dbContext.Admins.Remove(admin);
                await _dbContext.SaveChangesAsync();
                return admin;
            }
            catch (Exception)
            {
                Console.WriteLine("trash");
                throw;
            }
        }

      

      

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return admin;
        }
    }
}
