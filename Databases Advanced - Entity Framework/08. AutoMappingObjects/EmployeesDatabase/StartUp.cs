namespace EmployeesDatabase
{
    using AutoMapper;
    using EmployeesDatabase.Core;
    using EmployeesDatabase.Core.Contracts;
    using EmployeesDatabase.Core.Controllers;
    using EmployeesDatabase.Data;
    using EmployeesDatabase.Services;
    using EmployeesDatabase.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var service = ConfigureService();
            IEngine engine = new Engine(service);
            engine.Run(); 
        } 

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesDatabaseContext>(opts => opts.UseSqlServer(Configuration.connectionString));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<EmployeesDatabaseProfile>());

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();

            serviceCollection.AddTransient<IManagerController, ManagerController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
