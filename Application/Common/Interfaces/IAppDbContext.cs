using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Employee> Employees { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
