using Application.Common.Interfaces;
using Domain.Commons;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditableEntity();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditableEntity()
        {
            foreach (var entry in ChangeTracker.Entries<AudiTable>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
