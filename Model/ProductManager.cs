using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SpaManagement.Model
{
    public class ProductManager
    {
        public static ObservableCollection<PRODUCT> _DatabaseCustomers = new ObservableCollection<PRODUCT>(DataProvider.Ins.DB.PRODUCTs);

        public static ObservableCollection<PRODUCT> GetProducts()
        {
            return _DatabaseCustomers; 
        }

        public static void AddProduct(PRODUCT product)
        {
            _DatabaseCustomers.Add(product);   
        }
    }
}
