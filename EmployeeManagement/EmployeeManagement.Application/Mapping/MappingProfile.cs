using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<EmployeeSalary, EmployeeSalaryDto>().ReverseMap();
        }
    }
}
