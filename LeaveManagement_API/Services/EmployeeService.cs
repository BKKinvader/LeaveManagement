using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace LeaveManagement_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _dbContext.Employees.FindAsync(id);
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return employee;
            }
            catch (Exception )
            {
                Console.WriteLine("trash");
                throw;
            }
            
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            if (_dbContext == null)
            {
                Console.WriteLine("No employees found");
                return null;
            }
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            return employee;
        }

        public Task<Employee> GetEmployeeByName(string title)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
    }
}
