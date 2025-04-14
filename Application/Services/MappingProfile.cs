using Application.Models.CsvModels;
using Application.Models.EmployeeModels;
using Application.UseCases.EmployeeToDoList.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Globalization;

namespace Application.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<EmployeeViewModel, UpdateEmployeeCommand>().ReverseMap();

            CreateMap<EmployeeCsvModel, Employee>()
                .ForMember(dest => dest.Date_of_Birth, opt => opt.MapFrom(src => ParseDate(src.Date_of_Birth)))
                .ForMember(dest => dest.Start_Date, opt => opt.MapFrom(src => ParseDate(src.Start_Date)));
        }

        private static DateOnly ParseDate(string input)
        {
            var formats = new[] { "d/M/yyyy", "dd/MM/yyyy", "M/d/yyyy", "MM/dd/yyyy" };

            foreach (var format in formats)
            {
                if (DateOnly.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    return result;
            }

            if (DateOnly.TryParse(input, out var fallback))
                return fallback;

            throw new FormatException($"Unable to parse date: '{input}'");
        }

    }
}
