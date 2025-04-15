using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class UpdateEmployeeCommandHandler(
        IAppDbContext appDbContext,
        IMapper mapper
        ) : IRequestHandler<UpdateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IAppDbContext _dbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<EmployeeViewModel> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
                                               ?? throw new Exception($"User not found with Identifier {request.Id}");

            employee.Start_Date = request.Start_Date ?? employee.Start_Date;
            employee.Postcode = request.Postcode ?? employee.Postcode;
            employee.Forenames = request.Forenames ?? employee.Forenames;
            employee.Payroll_Number = request.Payroll_Number ?? employee.Payroll_Number;
            employee.Address = request.Address ?? employee.Address;
            employee.Address_2 = request.Address_2 ?? employee.Address_2;
            employee.Surname = request.Surname ?? employee.Surname;
            employee.Date_of_Birth = request.Date_of_Birth ?? employee.Date_of_Birth;
            employee.EMail_Home = request.EMail_Home ?? employee.EMail_Home;
            employee.Telephone = request.Telephone ?? employee.Telephone;
            employee.Mobile = request.Mobile ?? employee.Mobile;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EmployeeViewModel>(employee);
        }
    }
}
