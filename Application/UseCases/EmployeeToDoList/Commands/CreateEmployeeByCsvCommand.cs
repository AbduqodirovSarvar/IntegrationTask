using Application.Models.EmployeeModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class CreateEmployeeByCsvCommand : IRequest<List<EmployeeViewModel>>
    {
        [Required]
        public IFormFile CsvFile { get; set; } = null!;
    }
}
