using Application.Models.EmployeeModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
