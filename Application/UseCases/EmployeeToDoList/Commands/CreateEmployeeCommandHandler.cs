using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class CreateEmployeeCommandHandler(
        IAppDbContext appDbContext,
        IMapper mapper
        ) : IRequestHandler<CreateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IAppDbContext _dbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<EmployeeViewModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee newEmployee = _mapper.Map<Employee>(request);

            await _dbContext.Employees.AddAsync(newEmployee, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeViewModel>(newEmployee);
        }
    }
}
