using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    public class EmployeeManager
    {
        public static ObservableCollection<EMPLOYEE> _DatabaseEmployees = new ObservableCollection<EMPLOYEE>(DataProvider.Ins.DB.EMPLOYEEs);

        public static ObservableCollection<EMPLOYEE> GetEmployees()
        {
            return _DatabaseEmployees;
        }

        public static void AddEmployee(EMPLOYEE employee)
        {
            _DatabaseEmployees.Add(employee);

        }
    }
}
