﻿using AutoMapper;
using LeaveManagement_API.Model;
using LeaveManagement_API.Model.DTOs;

namespace LeaveManagement_API
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<LeaveBalance, LeaveBalanceDTO>().ReverseMap();

            CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();
        }
    }
}
