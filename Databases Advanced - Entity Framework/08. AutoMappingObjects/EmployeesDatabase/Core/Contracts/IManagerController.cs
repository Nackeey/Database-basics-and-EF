using EmployeesDatabase.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesDatabase.Core.Contracts
{
    public interface IManagerController
    {
        void SetManager(int employeeId, int managerId);

        ManagerDto ManagerInfo(int employeeId);
    }
}
