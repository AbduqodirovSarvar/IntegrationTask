using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.EmployeeToDoList.Commands
{
    public class DeleteEmployeeCommandHandler(
        IAppDbContext appDbContext
        ) : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IAppDbContext _dbContext = appDbContext;

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken)
                                               ?? throw new Exception($"User not found with Identifier {request.Id}");

            _dbContext.Employees.Remove(employee);
            return (await _dbContext.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
