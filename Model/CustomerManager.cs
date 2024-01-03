using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    public class CustomerManager
    {
        public static ObservableCollection<CUSTOMER> _DatabaseCustomers = new ObservableCollection<CUSTOMER>(DataProvider.Ins.DB.CUSTOMERs.Where(x=> x.IS_DELETED == false));

        public static ObservableCollection<CUSTOMER> GetCustomers()
        {
            return _DatabaseCustomers;
        }

        public static void AddCustomer(CUSTOMER customer)
        {
            _DatabaseCustomers.Add(customer);

        }

        public static void RemoveCustomer(CUSTOMER customer)
        {
            _DatabaseCustomers.Remove(customer);
        }
    }
}
