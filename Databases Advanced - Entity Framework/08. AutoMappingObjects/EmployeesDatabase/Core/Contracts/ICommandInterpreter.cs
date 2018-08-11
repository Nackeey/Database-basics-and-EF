using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesDatabase.Core.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] input);
    }
}
