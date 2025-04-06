using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Queries
{
    public class GetEmployeeQueryHandler(
        IAppDbContext appDbContext,
        IMapper mapper
        ) : IRequestHandler<GetEmployeeQuery, EmployeeViewModel>
    {
        private readonly IAppDbContext _dbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        async Task<EmployeeViewModel> IRequestHandler<GetEmployeeQuery, EmployeeViewModel>.Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
                                               ?? throw new Exception($"User not found with Identifier {request.Id}");

            return _mapper.Map<EmployeeViewModel>(employee);
        }
    }
}
