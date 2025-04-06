using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class CreateEmployeeByCsvCommandHandler(
        IAppDbContext appDbContext,
        IMapper mapper,
        IFileService fileService
        ) : IRequestHandler<CreateEmployeeByCsvCommand, List<EmployeeViewModel>>
    {
        private readonly IAppDbContext _dbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = fileService;

        public async Task<List<EmployeeViewModel>> Handle(CreateEmployeeByCsvCommand request, CancellationToken cancellationToken)
        {
            List<Employee> employeeList = await _fileService.ReadCsvFileAsync<Employee>(request.CsvFile);

            await _dbContext.Employees.AddRangeAsync(employeeList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<List<EmployeeViewModel>>(employeeList);
        }
    }
}
