using Application.Models.EmployeeModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
