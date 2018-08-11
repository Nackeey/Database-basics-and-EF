using AutoMapper.QueryableExtensions;
using EmployeesDatabase.Core.Contracts;
using EmployeesDatabase.Core.DTOs;
using EmployeesDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeesDatabase.Core.Controllers
{
    public class ManagerController : IManagerController
    {
        private readonly EmployeesDatabaseContext context;

        public ManagerController(EmployeesDatabaseContext context)
        {
            this.context = context;
        }

        public ManagerDto ManagerInfo(int employeeId)
        {
            var employee = context.Employees
                                  .Include(x => x.ManagerEmployees)
                                  .Where(x => x.EmployeeId == employeeId)
                                  .ProjectTo<ManagerDto>()
                                  .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentException("Invalid id");
            }

            return employee;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var manager = this.context.Employees.Find(managerId);

            if (employee == null || manager == null)
            {
                throw new ArgumentException("Invalid id");
            }

            employee.Manager = manager;

            context.SaveChanges();
        }
    }
}
