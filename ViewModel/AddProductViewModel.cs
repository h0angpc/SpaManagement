using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace SpaManagement.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {


        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private decimal _ProductPrice;
        public decimal ProductPrice { get => _ProductPrice; set { _ProductPrice = value; OnPropertyChanged(); } }

        private string _ProductLink;
        public string ProductLink { get => _ProductLink; set { _ProductLink = value; OnPropertyChanged(); } }


        private BitmapImage _ProductImage;
        public BitmapImage ProductImage { get => _ProductImage; set { _ProductImage = value; OnPropertyChanged(); } }
        public ICommand AddProductCommand { get; set; }   

        public AddProductViewModel()
        {

            AddProductCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ProductName))
                {
                    return false;
                }
                var displaylist = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_NAME == ProductName);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var Product = new PRODUCT() { PRO_NAME = ProductName, PRICE = ProductPrice };

                DataProvider.Ins.DB.PRODUCTs.Add(Product);
                DataProvider.Ins.DB.SaveChanges();

                ProductManager.AddProduct(Product);

                MessageBoxCustom m = new MessageBoxCustom("Thêm sản phẩm mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });

        }

    }
}
