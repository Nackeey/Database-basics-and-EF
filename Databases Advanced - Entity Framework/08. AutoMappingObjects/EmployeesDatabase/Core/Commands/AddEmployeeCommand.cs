namespace EmployeesDatabase.Core.Commands
{
    using EmployeesDatabase.Core.Contracts;
    using EmployeesDatabase.Core.DTOs;

    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeController controller;

        public AddEmployeeCommand(IEmployeeController employeeController)
        {
            this.controller = employeeController;
        }

        public string Execute(string[] args)
        {
            string firstname = args[0];
            string lastname = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDto employeeDto = new EmployeeDto
            {
                FirstName = firstname,
                LastName = lastname,
                Salary = salary
            };

            this.controller.AddEmployee(employeeDto);

            return $"Employee {firstname} {lastname} was added successfully";
        }
    }
}
