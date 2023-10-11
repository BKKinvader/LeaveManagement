using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeaveManagement_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }

        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and entity configurations here, if needed.
        }

        public void SeedData(AppDbContext appDbcontext)
        {
            if (!appDbcontext.Employees.Any())
            {
                // Add employees here
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        FullName = "John Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = "hashed_password", // You should hash passwords in a real application
                        Gender = "Male",
                        PhoneNumber = "123456789",
                        Role = "Employee"
                    },
                    // Add more employees as needed
                };
                    Employees.AddRange(employees);
                    appDbcontext.SaveChanges();
            }

            if (!appDbcontext.LeaveTypes.Any())
            {
                // Add leave types here
                var leaveTypes = new List<LeaveType>
                {
                    new LeaveType
                    {
                        TypeName = "Semester",
                        MaxDays = 25
                    },
                    new LeaveType
                    {
                        TypeName = "VAB",
                        MaxDays = 10
                    },
                    new LeaveType
                    {
                        TypeName = "Sjuk",
                        MaxDays = 5
                    },
                    // Add more leave types as needed
                    };
                LeaveTypes.AddRange(leaveTypes);
                appDbcontext.SaveChanges();
            }

            if (!appDbcontext.LeaveRequests.Any())
            {
                // Add leave requests here
                var leaveRequests = new List<LeaveRequest>
                {
                    new LeaveRequest
                    {
                        EmployeeId = Employees.First().Id, // Assign the ID of an existing employee
                        LeaveTypeId = LeaveTypes.First().Id, // Assign the ID of an existing leave type
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(5),
                        Status = LeaveRequest.LeaveStatus.Pending,
                        Comments = "Sample leave request"
                    },
                    // Add more leave requests as needed
                };
                LeaveRequests.AddRange(leaveRequests);
                appDbcontext.SaveChanges();
            }

            if (!appDbcontext.Admins.Any())
            {
                // Add admin here
                var admin = new Admin
                {
                    Username = "admin",
                    PasswordHash = "admin_password_hash", // Hashed admin password
                };
                Admins.Add(admin);
                appDbcontext.SaveChanges();
            }

            // Add LeaveBalance data here
            if (!appDbcontext.LeaveBalances.Any())
            {
                var leaveBalances = new List<LeaveBalance>
                {
                    new LeaveBalance
                    {
                        EmployeeId = appDbcontext.Employees.First().Id, // Assign the ID of an existing employee
                        LeaveTypeId = appDbcontext.LeaveTypes.First().Id, // Assign the ID of an existing leave type
                        RemainingDays = 20, // Specify the remaining days
                    },
                    // Add more LeaveBalance records as needed
                };
                appDbcontext.LeaveBalances.AddRange(leaveBalances);
                appDbcontext.SaveChanges();
            }

        }



    }
}