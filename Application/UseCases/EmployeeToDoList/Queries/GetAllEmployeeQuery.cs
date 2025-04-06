using Application.Models.EmployeeModels;
using Domain.Configurations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetAllEmployeeQuery : IRequest<List<EmployeeViewModel>>
    {
        public Filter Filter { get; set; } = new Filter();
        public PaginationParams PaginationParams { get; set; } = new PaginationParams();
    }
}
