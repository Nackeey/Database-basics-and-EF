using EmployeesDatabase.Core.Contracts;
using System;
using System.Threading;

namespace EmployeesDatabase.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i >= 1; i--)
            {
                Console.WriteLine($"This program will close after {i} seconds");
                Thread.Sleep(1000);
            }

            Environment.Exit(0);

            return null;
        }
    }
}
