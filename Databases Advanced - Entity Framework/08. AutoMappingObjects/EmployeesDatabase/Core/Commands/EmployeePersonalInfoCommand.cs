using EmployeesDatabase.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesDatabase.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public EmployeePersonalInfoCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            var employeePersonalInfoDto = this.employeeController.GetEmployeePersonalInfo(id);

            return $"ID: {employeePersonalInfoDto.EmployeeId} - {employeePersonalInfoDto.FirstName} {employeePersonalInfoDto.LastName} - ${employeePersonalInfoDto.Salary:F2}\n" +
                $"Birthday: {employeePersonalInfoDto.Birthday.Value.ToString("dd-MM-yyyy")}\n" +
                $"Address: {employeePersonalInfoDto.Address}";
        }
    }
}
