using Application.Models.EmployeeModels;
using Domain.Configurations;
using MediatR;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetAllEmployeeQuery : IRequest<ResponseViewModel<List<EmployeeViewModel>>>
    {
        public Filter Filter { get; set; } = new Filter();
        public PaginationParams PaginationParams { get; set; } = new PaginationParams();
    }
}
