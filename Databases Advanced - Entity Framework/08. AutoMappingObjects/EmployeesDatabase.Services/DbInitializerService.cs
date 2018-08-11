namespace EmployeesDatabase.Services
{
    using EmployeesDatabase.Data;
    using EmployeesDatabase.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DbInitializerService : IDbInitializerService
    {
        private readonly EmployeesDatabaseContext context;

        public DbInitializerService(EmployeesDatabaseContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
