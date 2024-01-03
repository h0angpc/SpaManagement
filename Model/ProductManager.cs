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
        public static ObservableCollection<PRODUCT> _DatabaseProduct = new ObservableCollection<PRODUCT>(DataProvider.Ins.DB.PRODUCTs.Where(p => p.IS_DELETED == false));

        public static ObservableCollection<PRODUCT> GetProducts()
        {
            return _DatabaseProduct; 
        }

        public static void AddProduct(PRODUCT product)
        {
            _DatabaseProduct.Add(product);   
        }
        public static void RemoveProduct(PRODUCT product)
        {
            _DatabaseProduct.Remove(product);
        }
    }
}
