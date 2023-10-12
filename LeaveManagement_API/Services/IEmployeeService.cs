using LeaveManagement_API.Model;

namespace LeaveManagement_API.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeById(int id);
        public Task<Employee> GetEmployeeByName(string name);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);

        public Task SaveAsync();
    }
}
