namespace EmployeesDatabase.Core.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using EmployeesDatabase.Core.Contracts;
    using EmployeesDatabase.Core.DTOs;
    using EmployeesDatabase.Data;
    using EmployeesDatabase.Models;
    using System;
    using System.Linq;

    public class EmployeeController : IEmployeeController
    {
        private readonly EmployeesDatabaseContext context;
        private readonly IMapper mapper;

        public EmployeeController(EmployeesDatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeeDto>(employee);    

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeePersonalInfoDto>(employee);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Address = address;

            context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Birthday = date;

            context.SaveChanges();
        }
    }
}
