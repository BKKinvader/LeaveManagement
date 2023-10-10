using LeaveManagement_API.Model;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
        public DbSet<LeaveRequest> LeaveRequests{get;set;}
        public DbSet<LeaveType> LeaveTypes{get;set;}
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera relationer och annan konfiguration här om det behövs.
        }

        public void SeedData(AppDbContext context)
        {
            if (!context.Employees.Any())
            {
                // Lägg till fem Employee-objekt med Guid som EmployeeId
                var employees = new List<Employee>
        {
            new Employee
            {
                Id = Guid.NewGuid(), // Generera ett unikt ID
                FullName = "Anställd 1",
                Email = "anstalld1@example.com",
                Password = "password1", 
                Gender = "Man",
                PhoneNumber = "123456789",
                Role = "Employee"
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Anställd 2",
                Email = "anstalld2@example.com",
                Password = "password2",
                Gender = "Kvinna",
                PhoneNumber = "987654321",
                Role = "Employee"
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Anställd 3",
                Email = "anstalld3@example.com",
                Password = "password3",
                Gender = "Man",
                PhoneNumber = "555555555",
                Role = "Employee"
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Anställd 4",
                Email = "anstalld4@example.com",
                Password = "password4",
                Gender = "Kvinna",
                PhoneNumber = "444444444",
                Role = "Employee"
            },
            new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Anställd 5",
                Email = "anstalld5@example.com",
                Password = "password5",
                Gender = "Man",
                PhoneNumber = "222222222",
                Role = "Employee"
            },
        };

                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            if (!context.LeaveTypes.Any())
            {
                // Lägg till några LeaveType-objekt
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
        };

                context.LeaveTypes.AddRange(leaveTypes);
                context.SaveChanges();
            }

            if (!context.LeaveRequests.Any())
            {
                // Hämta Employee-objekten för att använda deras Guids som EmployeeId i LeaveRequest
                var employees = context.Employees.ToList();

                // Lägg till fem olika LeaveRequest-objekt med olika EmployeeId och LeaveTypeId som Guid
                var leaveRequests = new List<LeaveRequest>
        {
            new LeaveRequest
            {
                EmployeeId = employees[0].Id,
                LeaveTypeId = 1, // LeaveType: Semester
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Status = LeaveRequest.LeaveStatus.Pending
            },
            new LeaveRequest
            {
                EmployeeId = employees[1].Id,
                LeaveTypeId = 2, // LeaveType: VAB
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                Status = LeaveRequest.LeaveStatus.Pending
            },
            new LeaveRequest
            {
                EmployeeId = employees[2].Id,
                LeaveTypeId = 3, // LeaveType: Sjuk
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                Status = LeaveRequest.LeaveStatus.Pending
            },
            new LeaveRequest
            {
                EmployeeId = employees[3].Id,
                LeaveTypeId = 1, // LeaveType: Semester
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                Status = LeaveRequest.LeaveStatus.Pending
            },
            new LeaveRequest
            {
                EmployeeId = employees[4].Id,
                LeaveTypeId = 2, // LeaveType: VAB
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(4),
                Status = LeaveRequest.LeaveStatus.Pending
            },
        };

                context.LeaveRequests.AddRange(leaveRequests);
                context.SaveChanges();
            }

            if (!context.Admins.Any())
            {
                // Lägg till en admin
                var admin = new Admin
                {
                    Id = Guid.NewGuid(), // Generera ett unikt ID
                    Username = "admin",
                    Password = "hashedadminpassword", // Använd hashat lösenord
                    IsAdmin = true
                };

                context.Admins.Add(admin);
                context.SaveChanges();
            }
        }

    }
}