using AutoMapper;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Services;
using System.Net;

namespace LeaveManagement_API.Endpoint
{
    public static class LeaveBalanceEndpoint
    {

        public static void ConfigureLeaveBalanceEndPoints(this WebApplication app)
        {
            app.MapGet("/api/LeaveBalance/", GetAllLeaveBalance).WithName("GetLeaveBalance").Produces<APIResponse>(200);
            app.MapGet("/api/LeaveBalance/{id:int}", GetLeaveBalanceById).WithName("GetLeaveBalanceById").Produces<APIResponse>(200);
            app.MapPost("/api/LeaveBalance", CreateLeaveBalance).WithName("CreateLeaveBalance").Accepts<LeaveRequest>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/LeaveBalance/{id:int}", UpdateLeaveBalance).WithName("UpdateELeaveBalance").Accepts<LeaveRequest>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/LeaveBalance/{id:int}", DeleteLeaveBalanceById).WithName("DeleteLeaveBalance").Produces<APIResponse>(200);
        }


        private async static Task<IResult> GetAllLeaveBalance(ILeaveBalanceService _leaveBalanceService)
        {
            APIResponse response = new();
            response.Result = await _leaveBalanceService.GetAllLeaveBalance();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetLeaveBalanceById(ILeaveBalanceService _leaveBalanceService, int id)
        {
            APIResponse response = new();
            response.Result = await _leaveBalanceService.GetLeaveBalanceById(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreateLeaveBalance(ILeaveBalanceService _leaveBalanceService, IMapper _mapper, LeaveBalanceDTO leaveBalanceDTO)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
            LeaveBalance leaveBalance = _mapper.Map<LeaveBalance>(leaveBalanceDTO);
            response.Result = await _leaveBalanceService.CreateLeaveBalance(leaveBalance);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response.Result);

        }

        private async static Task<IResult> UpdateLeaveBalance(ILeaveBalanceService leaveBalanceService, IMapper _mapper, LeaveBalance leaveBalance, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            LeaveBalance leaveBalanceUpdate = await leaveBalanceService.GetLeaveBalanceById(id);
            leaveBalanceUpdate.EmployeeId = leaveBalance.EmployeeId;
            leaveBalanceUpdate.LeaveTypeId = leaveBalance.LeaveTypeId;
            leaveBalanceUpdate.RemainingDays = leaveBalance.RemainingDays;
            

            //response
            response.Result = await leaveBalanceService.UpdateLeaveBalance(leaveBalanceUpdate);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteLeaveBalanceById(ILeaveBalanceService leaveBalanceService, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            LeaveBalance leaveBalance = await leaveBalanceService.GetLeaveBalanceById(id);
            if (leaveBalance != null)
            {
                await leaveBalanceService.DeleteLeaveBalance(id);
                await leaveBalanceService.SaveAsync();
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
