using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Employees.Server.Data.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
    }

    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder
                .ToTable("Employee");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(p => p.Name)
                .HasMaxLength(50);

            builder
                .Property(p => p.Phone)
                .HasMaxLength(20);
        }
    }
}
