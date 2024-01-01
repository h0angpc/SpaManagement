using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    public class ReceiptManager
    {
        public static ObservableCollection<RECEIPT> _DatabaseReceipts = new ObservableCollection<RECEIPT>(DataProvider.Ins.DB.RECEIPTs);

        public static ObservableCollection<RECEIPT> GetReceipt()
        {
            return _DatabaseReceipts;
        }
        public static void AddReceipt(RECEIPT receipt)
        {
            _DatabaseReceipts.Add(receipt);
        }
        public static void RemoveReceipt(RECEIPT receipt)
        {
            _DatabaseReceipts.Remove(receipt);
        }
    }
}
