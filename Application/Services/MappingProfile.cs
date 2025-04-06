using Application.Models.EmployeeModels;
using Application.UseCases.EmployeeToDoList.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.PayrollNumber, opt => opt.MapFrom(src => src.Payroll_Number))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Forenames + " " + src.Surname))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail_Home))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start_Date));
            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();
        }
    }
}
