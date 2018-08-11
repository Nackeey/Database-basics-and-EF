using EmployeesDatabase.Core.Contracts;
using EmployeesDatabase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesDatabase.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly IManagerController controller;

        public ManagerInfoCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerDto = this.controller.ManagerInfo(employeeId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesCount}");

            foreach (var employee in managerDto.EmployeeDtos)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
