using AutoMapper;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Services;
using System.Net;

namespace LeaveManagement_API.Endpoint
{
    public static class LeaveTypeEndpoints
    {
        public static void ConfigureLeaveTypeEndpoints(this WebApplication app)
        {
            app.MapGet("/api/leavetypes", GetAllLeaveTypes).Produces<APIResponse>(200);
            app.MapGet("/api/leavetypes/{id:int}", GetLeaveTypeById).Produces<APIResponse>(200);
            app.MapPost("/api/leavetypes", CreateLeaveType).Accepts<LeaveTypeDTO>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/leavetypes/{id:int}", UpdateLeaveType).Accepts<LeaveTypeDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/leavetypes/{id:int}", DeleteLeaveTypeById).Produces<APIResponse>(200);
        }

        private async static Task<IResult> GetAllLeaveTypes(ILeaveTypeService leaveTypeService)
        {
            APIResponse response = new();
            response.Result = await leaveTypeService.GetAllLeaveTypes();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetLeaveTypeById(ILeaveTypeService leaveTypeService, int id)
        {
            APIResponse response = new();
            response.Result = await leaveTypeService.GetLeaveTypeById(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreateLeaveType(ILeaveTypeService leaveTypeService, IMapper mapper, LeaveTypeDTO leaveTypeDTO)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
            LeaveType leaveType = mapper.Map<LeaveType>(leaveTypeDTO);
            response.Result = await leaveTypeService.AddLeaveType(leaveType);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateLeaveType(ILeaveTypeService leaveTypeService, IMapper mapper, LeaveTypeDTO leaveTypeDTO, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            LeaveType leaveTypeUpdate = await leaveTypeService.GetLeaveTypeById(id);
            if (leaveTypeUpdate != null)
            {
                leaveTypeUpdate.TypeName = leaveTypeDTO.TypeName;
                leaveTypeUpdate.MaxDays = leaveTypeDTO.MaxDays;
            }

            
            response.Result = await leaveTypeService.UpdateLeaveType(leaveTypeUpdate);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteLeaveTypeById(ILeaveTypeService leaveTypeService, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            LeaveType leaveTypeFromDB = await leaveTypeService.GetLeaveTypeById(id);
            if (leaveTypeFromDB != null)
            {
                await leaveTypeService.DeleteLeaveType(id);
                await leaveTypeService.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid ID");
                return Results.BadRequest(response);
            }
        }
    }


}
