using Application.Models.EmployeeModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class UpdateEmployeeCommand : IRequest<EmployeeViewModel>
    {
        [Required]
        public Guid Id { get; set; }

        public string? Payroll_Number { get; set; } = null;
        public string? Forenames { get; set; } = null;
        public string? Surname { get; set; } = null;
        public DateOnly? Date_of_Birth { get; set; } = null;
        public string? Telephone { get; set; } = null;
        public string? Mobile { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? Address_2 { get; set; } = null;
        public string? Postcode { get; set; } = null;
        public string? EMail_Home { get; set; } = null;
        public DateOnly? Start_Date { get; set; } = null;
    }
}
