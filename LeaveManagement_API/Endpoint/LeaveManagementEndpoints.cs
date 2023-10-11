using AutoMapper;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace LeaveManagement_API.Endpoints
{
    public static class LeaveManagementEndpoints
    {
        public static void ConfigureEmployeeEndPoints(this WebApplication app)
        {
            app.MapGet("/api/emplooyee/", GetAllEmployees).WithName("GetEmployees").Produces<APIResponse>(200);
            app.MapGet("/api/employees/{id:int}", GetEmployeeById).WithName("GetEmployeeById").Produces<APIResponse>(200);
            app.MapPost("/api/employee", CreateEmployee).WithName("CreateEmployee").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/employees/{id:int}", UpdateEmployee).WithName("UpdateEmployee").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/employees/{id:int}", DeleteEmployeeById).WithName("DeleteEmployee").Produces<APIResponse>(200);
        }


        private async static Task<IResult> GetAllEmployees(IEmployeeService _employeeService)
        {
            APIResponse response = new();
            response.Result = await _employeeService.GetAllEmployees();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetEmployeeById(IEmployeeService _employeeService, int id)
        {
            APIResponse response = new();
            response.Result = await _employeeService.GetEmployeeById(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreateEmployee(IEmployeeService _employeeService, IMapper _mapper, EmployeeDTO employeeDTO)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
            };
            Employee employee = _mapper.Map<Employee>(employeeDTO);
            response.Result = await _employeeService.AddEmployee(employee);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);

        }

        private async static Task<IResult> UpdateEmployee(IEmployeeService _employeeService, IMapper _mapper, EmployeeDTO employeeDTO, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            Employee employeeUpdate = await _employeeService.GetEmployeeById(id);
            employeeUpdate.FullName = employeeDTO.FullName;
            employeeUpdate.Email = employeeDTO.Email;
            employeeUpdate.PasswordHash = employeeUpdate.PasswordHash;
            employeeUpdate.Gender = employeeDTO.Gender;
            employeeUpdate.PhoneNumber = employeeDTO.PhoneNumber;
            employeeUpdate.Role = employeeDTO.Role;

            //Mapping
            Employee employee = _mapper.Map<Employee>(employeeDTO);

            //response
            response.Result = await _employeeService.UpdateEmployee(employeeUpdate);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteEmployeeById(IEmployeeService _employeeService, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            Employee EmployeeFromDB = await _employeeService.GetEmployeeById(id);
            if (EmployeeFromDB != null)
            {
                await _employeeService.DeleteEmployee(id);
                await _employeeService.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invaid ID");
                return Results.BadRequest(response);
            }
        }


    }
}

//Endpoints from Program.cs
//app.MapGet("/api/employees/", async ([FromServices] IEmployeeService _employeeService) =>
//{
//    APIResponse response = new();
//    response.Result = await _employeeService.GetAllEmployees();
//    response.IsSuccess = true;
//    response.StatusCode = HttpStatusCode.OK;
//    return Results.Ok(response);

//}).WithName("GetEmployees").Produces<APIResponse>(200);


//app.MapGet("/api/employees/{id:int}", async ([FromServices] IEmployeeService _employeeService, int id) =>
//{
//    var employee = await _employeeService.GetEmployeeById(id);
//    if (employee == null)
//    {
//        return Results.NotFound("There are no employees");
//    }
//    return Results.Ok(employee);
//});

//app.MapPost("/api/employee", async (IEmployeeService _employeeService, IMapper _mapper, EmployeeDTO employeeDto) =>
//{
//    APIResponse response = new()
//    {
//        IsSuccess = true,
//        StatusCode = HttpStatusCode.OK,
//    };

//    Employee employee = _mapper.Map<Employee>(employeeDto);
//    response.Result = await _employeeService.AddEmployee(employee);
//    response.IsSuccess = true;
//    response.StatusCode = HttpStatusCode.Created;
//    return Results.Ok(response);

//}).WithName("CreateBook").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(201).Produces(400);

//app.MapPut("/api/employees/{id:int}", async (IEmployeeService _employeeService, IMapper _mapper, EmployeeDTO employeeDto, int id) =>
//{
//    APIResponse response = new()
//    {
//        IsSuccess = true,
//        StatusCode = HttpStatusCode.OK,
//    };

//    Employee employeeUpdate = await _employeeService.GetEmployeeById(id);

//    employeeUpdate.FullName = employeeDto.FullName;
//    employeeUpdate.Email = employeeDto.Email;
//    employeeUpdate.PasswordHash = employeeUpdate.PasswordHash;
//    employeeUpdate.Gender = employeeDto.Gender;
//    employeeUpdate.PhoneNumber = employeeDto.PhoneNumber;
//    employeeUpdate.Role = employeeDto.Role;

//    //Mapping
//    Employee employee = _mapper.Map<Employee>(employeeDto);

//    //response
//    response.Result = await _employeeService.UpdateEmployee(employeeUpdate);
//    response.IsSuccess = true;
//    response.StatusCode = HttpStatusCode.OK;
//    return Results.Ok(response);
//}).WithName("UpdateEmployee").Accepts<EmployeeDTO>("application/json").Produces<APIResponse>(200).Produces(400);

//app.MapDelete("/api/employees/{id:int}", async (IEmployeeService _employeeService, int id) =>
//{
//    APIResponse response = new()
//    {
//        IsSuccess = false,
//        StatusCode = HttpStatusCode.BadRequest
//    };

//    var employee = await _employeeService.DeleteEmployee(id);
//    if ( employee == null)
//    {
//        return Results.NotFound("Cannot find book in the database");
//    }

//    response.Result = await _employeeService.DeleteEmployee(id);
//    response.IsSuccess = true;
//    response.StatusCode = HttpStatusCode.NotFound;
//    return Results.Ok(response);
//});