using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesDatabase.Services.Contracts
{
    public interface IDbInitializerService
    {
        void InitializeDatabase();
    }
}
