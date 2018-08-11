namespace EmployeesDatabase.Core.Commands
{
    using Core.Contracts;

    public class SetManagerCommand : ICommand
    {
        private readonly IManagerController controller;

        public SetManagerCommand(IManagerController controller)
        {
            this.controller = controller;
        }

        public string Execute(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            this.controller.SetManager(employeeId, managerId);

            return "Command accomplished successfully";
        }
    }
}
