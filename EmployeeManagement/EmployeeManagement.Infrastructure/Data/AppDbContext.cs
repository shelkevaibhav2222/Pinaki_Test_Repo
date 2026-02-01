using EmployeeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(x => x.FirstName).HasMaxLength(100).HasColumnType("nvarchar(100)");
                entity.Property(x => x.LastName).HasMaxLength(100).HasColumnType("nvarchar(100)");
                entity.Property(x => x.City).HasMaxLength(100).HasColumnType("nvarchar(100)");
                entity.Property(x => x.Zip).HasMaxLength(10).HasColumnType("nvarchar(10)");

                entity
                    .Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<EmployeeSalary>(entity =>
            {
                entity.ToTable("EmployeeSalary");

                entity.Property(x => x.Amount).HasPrecision(18, 2);

                entity.Property(x => x.SalaryDate).HasColumnType("datetime");

                entity
                    .Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                entity
                    .HasOne(x => x.Employee)
                    .WithMany(x => x.Salaries)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
