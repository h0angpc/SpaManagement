

using Microsoft.Win32;
using SpaManagement.Model;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Security.Policy;
using System.Xml.Linq;
using SpaManagement.Views;

namespace SpaManagement.ViewModel
{
    public class EditProductViewModel : BaseViewModel
    {
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private decimal _ProductPrice;
        public decimal ProductPrice { get => _ProductPrice; set { _ProductPrice = value; OnPropertyChanged(); } }

        private string _ProductLink;
        public string ProductLink { get => _ProductLink; set { _ProductLink = value; OnPropertyChanged(); } }

        private BitmapImage _ProductImage;
        public BitmapImage ProductImage { get => _ProductImage; set { _ProductImage = value; OnPropertyChanged(); } }
        public ICommand EditProductCommand { get; set; }
        public ICommand ImageProduct { get; set; }

        private Uri tempUri;

        private string tempIMG;
        public ICommand CloseCommand { get; set; }
        public EditProductViewModel(PRODUCT SelectedProduct) 
        {
            ProductName = SelectedProduct.PRO_NAME;
            ProductPrice = SelectedProduct.PRICE;
            ProductLink = SelectedProduct.PRO_URL;
            tempUri = new Uri(SelectedProduct.PRO_IMG);
            ProductImage = new BitmapImage(tempUri);
            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            ImageProduct = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Title = "Vui lòng chọn ảnh.";
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    ProductImage = new BitmapImage(fileUri);
                    tempIMG = ProductImage.ToString(); // Gáng tạm
                    string sourcefile = openFileDialog.FileName;
                    string resourceUri = "..//..//Image//" + System.IO.Path.GetFileName(openFileDialog.FileName);
                    var list1 = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_IMG == resourceUri);
                    if (list1 != null)
                    {

                    }
                    else
                    {
                        System.IO.File.Copy(sourcefile, resourceUri, true);// Nếu chưa có thì lưu ảnh
                    }
                }
            });
            EditProductCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(ProductPrice.ToString()) || string.IsNullOrEmpty(ProductLink) || ProductImage == null)
                {
                    return false;
                }

                var displaylist = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_NAME == ProductName && x.PRICE == ProductPrice && x.PRO_URL == ProductLink && x.PRO_IMG == tempIMG); // nếu chưa thay đổi gì so với cái cũ thì button không được bật
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var product = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_ID == SelectedProduct.PRO_ID).SingleOrDefault();


                product.PRO_NAME = ProductName;
                product.PRICE = ProductPrice;
                product.PRO_URL = ProductLink;
                product.PRO_IMG = ProductImage.ToString();

                DataProvider.Ins.DB.SaveChanges();

                SelectedProduct.PRO_NAME = ProductName;
                SelectedProduct.PRO_URL = ProductLink;
                SelectedProduct.PRO_IMG = ProductImage.ToString();
                SelectedProduct.PRICE = ProductPrice;


                MessageBoxCustom m = new MessageBoxCustom("Cập nhật sản phẩm mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });

        }

    }
}
