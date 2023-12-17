using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    internal class PaymentManager
    {
        public static ObservableCollection<PAYMENT> _DatabasePayments = new ObservableCollection<PAYMENT>(DataProvider.Ins.DB.PAYMENTs);

        public static ObservableCollection<PAYMENT> GetPayment()
        {
            return _DatabasePayments;
        }
        public static ObservableCollection<CUSTOMER> _DatabaseCustomers = new ObservableCollection<CUSTOMER>(DataProvider.Ins.DB.CUSTOMERs);

        public static ObservableCollection<CUSTOMER> GetCustomers()
        {
            return _DatabaseCustomers;
        }
        public static void AddPayment(PAYMENT payment)
        {
           _DatabasePayments.Add(payment);

        }
    }
}
