using EmployeesDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeesDatabase.Data
{
    public class EmployeesDatabaseContext : DbContext
    {
        public EmployeesDatabaseContext()
        {}

        public EmployeesDatabaseContext(DbContextOptions contextOptions)
            : base (contextOptions)
        {}

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(x => x.Manager)
                      .WithMany(x => x.ManagerEmployees)
                      .HasForeignKey(x => x.ManagerId);
            });
        }
    }
}
