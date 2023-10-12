using AutoMapper;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;
using LeaveManagement_API.Services;
using System.Net;

namespace LeaveManagement_API.Endpoint
{
    public static class AdminEndpoins
    {


        public static void ConfigureAdminEndPoints(this WebApplication app)
        {
            app.MapGet("/api/admin/", GetAllAdmins).WithName("GetAdmins").Produces<APIResponse>(200);
            app.MapGet("/api/admins/{id:int}", GetAdminById).WithName("GetAdminById").Produces<APIResponse>(200);
            app.MapPost("/api/admin", CreateAdmin).WithName("CreateAdmin").Accepts<AdminDTO>("application/json").Produces<APIResponse>(201).Produces(400);
            app.MapPut("/api/admins/{id:int}", UpdateAdmin).WithName("UpdateAdmin").Accepts<AdminDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("/api/admins/{id:int}", DeleteAdminById).WithName("DeleteAdmin").Produces<APIResponse>(200);
        }





        private async static Task<IResult> GetAllAdmins(IAdminService _adminService)
        {
            APIResponse response = new();
            response.Result = await _adminService.GetAllAdmin();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetAdminById(IAdminService _adminService, int id)
        {
            APIResponse response = new();
            response.Result = await _adminService.GetAdminById(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreateAdmin(IAdminService _adminService, IMapper _mapper, AdminDTO adminDTO)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };
            Admin admin = _mapper.Map<Admin>(adminDTO);
            response.Result = await _adminService.AddAdmin(admin);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);

        }

        private async static Task<IResult> UpdateAdmin(IAdminService _adminService, IMapper _mapper, AdminDTO adminDTO, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
            };

            Admin adminUpdate = await _adminService.GetAdminById(id);
            adminUpdate.Username = adminDTO.Username;
            adminUpdate.PasswordHash = adminDTO.PasswordHash;
           

            //Mapping
            Admin admin = _mapper.Map<Admin>(adminDTO);

            //response
            response.Result = await _adminService.UpdateAdmin(adminUpdate);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteAdminById(IAdminService _adminService, int id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            Admin adminFromDB = await _adminService.GetAdminById(id);
            if (adminFromDB != null)
            {
                await _adminService.DeleteAdmin(id);
                await _adminService.SaveAsync();
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
