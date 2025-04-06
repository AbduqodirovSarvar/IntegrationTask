using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
