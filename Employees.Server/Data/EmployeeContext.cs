using Employees.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.Server.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        }
    }
}
