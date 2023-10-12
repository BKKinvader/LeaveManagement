using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<Admin>> GetAllAdmin();
        public Task<Admin> GetAdminById(int id);
        public Task<Admin> AddAdmin(Admin admin);
        public Task<Admin> UpdateAdmin(Admin admin);
        public Task<Admin> DeleteAdmin(int id);

        public Task SaveAsync();
    }
}
