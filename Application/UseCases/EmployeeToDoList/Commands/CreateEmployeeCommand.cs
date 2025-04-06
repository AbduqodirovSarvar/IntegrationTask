﻿using Application.Models.EmployeeModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class CreateEmployeeCommand : IRequest<EmployeeViewModel>
    {
        public string Payroll_Number { get; set; } = string.Empty;
        public string Forenames { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateOnly Date_of_Birth { get; set; } = DateOnly.MinValue;
        public string Telephone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address_2 { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string EMail_Home { get; set; } = string.Empty;
        public DateOnly Start_Date { get; set; } = DateOnly.MinValue;
    }
}
