using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
