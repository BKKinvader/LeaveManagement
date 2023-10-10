using AutoMapper;
using LeaveManagement_API.Data;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LeaveManagement_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });

            


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();

                // Ensure the database is created or migrated
                context.Database.Migrate();

                // Call the SeedData method to populate data
                context.SeedData(context);
            }

            app.MapGet("/api/employees/", async ([FromServices] IEmployeeService _employeeService) =>
            {
                APIResponse response = new();
                response.Result = await _employeeService.GetAllEmployees();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);

            }).WithName("GetEmployees").Produces<APIResponse>(200);


            app.MapGet("/api/employees/{id:Guid}", async ([FromServices] IEmployeeService _employeeService, Guid id) =>
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return Results.NotFound("There are no employees");
                }
                return Results.Ok(employee);
            });

            app.MapPost("/api/employee", async (IEmployeeService _employeeService,IMapper _mapper, EmployeeDTO employeeDto) =>
            {
                APIResponse response = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                };

                Employee employee = _mapper.Map<Employee>(employeeDto);
                response.Result = await _employeeService.AddEmployee(employee);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                return Results.Ok(response);

            }).WithName("CreateBook").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(201).Produces(400);

            app.MapPut("/api/employees/{id:Guid}", async (IEmployeeService _employeeService, IMapper _mapper, EmployeeDTO employeeDto, Guid id) =>
            {
                APIResponse response = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                };

                Employee employeeUpdate = await _employeeService.GetEmployeeById(id);

                employeeUpdate.FullName = employeeDto.FullName;
                employeeUpdate.Email = employeeDto.Email;
                employeeUpdate.PasswordHash = employeeUpdate.PasswordHash;
                employeeUpdate.Gender = employeeDto.Gender;
                employeeUpdate.PhoneNumber = employeeDto.PhoneNumber;
                employeeUpdate.Role = employeeDto.Role;

                //Mapping
                Employee employee = _mapper.Map<Employee>(employeeDto);

                //response
                response.Result = await _employeeService.UpdateEmployee(employeeUpdate);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }).WithName("UpdateEmployee").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(200).Produces(400);

            app.MapDelete("/api/employees/{id:Guid}", async (IEmployeeService _employeeService, Guid id) =>
            {
                APIResponse response = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                };

                var employee = await _employeeService.DeleteEmployee(id);
                if ( employee == null)
                {
                    return Results.NotFound("Cannot find book in the database");
                }

                response.Result = await _employeeService.DeleteEmployee(id);
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NotFound;
                return Results.Ok(response);
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

          

            app.Run();
        }
    }
}