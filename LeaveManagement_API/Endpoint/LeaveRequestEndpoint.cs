using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Model;
using LeaveManagement_API.Services;
using System.Net;
using AutoMapper;

namespace LeaveManagement_API.Endpoint
{
    public static class LeaveRequestEndpoint
    {
        public static void ConfigureLeaveRequestEndPoints(this WebApplication app)
        {
            app.MapGet("/api/LeaveRequest/", GetAllLeaveRequests).WithName("GetLeaveRequest").Produces<APIResponse>(200);
            app.MapGet("/api/LeaveRequest/{id:int}", GetLeaveRequestById).WithName("GetLeaveRequestById").Produces<APIResponse>(200);
            app.MapPost("/api/LeaveRequest", CreateLeaveRequest).WithName("CreateLeaveRequest").Accepts<LeaveRequestDTO>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/LeaveRequest/{id:int}", UpdateLeaveRequest).WithName("UpdateELeaveRequest").Accepts<LeaveRequestDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/LeaveRequest/{id:int}", DeleteLeaveRequestById).WithName("DeleteLeaveRequest").Produces<APIResponse>(200);
        }

        private async static Task<IResult> GetAllLeaveRequests(ILeaveRequestService _leaveRequestService)
        {
            APIResponse response = new();
            response.Result = await _leaveRequestService.GetAllLeaveRequests();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Results.Ok(response);
        }

        private async static Task<IResult> GetLeaveRequestById(ILeaveRequestService _leaveRequestService, int id)
        {
            APIResponse response = new();
            response.Result = await _leaveRequestService.GetLeaveRequestById(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreateLeaveRequest(ILeaveRequestService _leaveRequestService, IMapper _mapper, LeaveRequestDTO leaveRequestDTO)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            LeaveRequest leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDTO);
            response.Result = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);

        }

        private async static Task<IResult> UpdateLeaveRequest(ILeaveRequestService leaveRequestService, IMapper _mapper, LeaveRequest leaveRequest, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            LeaveRequest leaveRequestUpdate = await leaveRequestService.GetLeaveRequestById(id);
            leaveRequestUpdate.EmployeeId = leaveRequest.EmployeeId;
            leaveRequestUpdate.LeaveTypeId = leaveRequest.LeaveTypeId;
            leaveRequestUpdate.StartDate = leaveRequest.StartDate;
            leaveRequestUpdate.EndDate = leaveRequest.EndDate;
            leaveRequestUpdate.Status = leaveRequest.Status;
            leaveRequestUpdate.Comments = leaveRequest.Comments;

            //response
            response.Result = await leaveRequestService.UpdateLeaveRequest(leaveRequestUpdate);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteLeaveRequestById(ILeaveRequestService leaveRequestService, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            LeaveRequest leaveRequest = await leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest != null)
            {
                await leaveRequestService.DeleteLeaveRequest(id);
                await leaveRequestService.SaveAsync();
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
