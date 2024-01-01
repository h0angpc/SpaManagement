using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    public class PaymentManager
    {
        public static ObservableCollection<PAYMENT> _DatabasePayments = new ObservableCollection<PAYMENT>(DataProvider.Ins.DB.PAYMENTs);

        public static ObservableCollection<PAYMENT> GetPayment()
        {
            return _DatabasePayments;
        }
        public static void AddPayment(PAYMENT payment)
        {
           _DatabasePayments.Add(payment);
        }
        public static void RemovePayment(PAYMENT payment)
        {
            _DatabasePayments.Remove(payment);
        }
    }
}
