using Microsoft.Win32;
using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace SpaManagement.ViewModel
{
    public class EditProductViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private string _ProductPrice;
        public string ProductPrice
        {
            get => _ProductPrice;
            set
            {
                _ProductPrice = value;
                _errorsViewModel.ClearErrors(nameof(ProductPrice));
                if (!IsNumeric(_ProductPrice.Replace(",", "")) && _ProductPrice != "")
                {
                    _errorsViewModel.AddError(nameof(ProductPrice), "Giá sản phẩm chỉ chứa những con số");
                }

                decimal num = decimal.Parse(_ProductPrice);
                _ProductPrice = string.Format("{0:N0}", num);


                OnPropertyChanged(nameof(ProductPrice));
            }
        }

        private string _ProductLink;
        public string ProductLink
        {
            get => _ProductLink;
            set
            {
                _ProductLink = value;
                _errorsViewModel.ClearErrors(nameof(ProductLink));
                if (!IsValidUri(_ProductLink) && _ProductLink != "")
                {
                    _errorsViewModel.AddError(nameof(ProductLink), "Link sản phẩm không hợp lệ, hãy thử lại");
                }

                OnPropertyChanged(nameof(ProductLink));
            }
        }
        private BitmapImage _ProductImage;
        public BitmapImage ProductImage { get => _ProductImage; set { _ProductImage = value; OnPropertyChanged(); } }
        public ICommand EditProductCommand { get; set; }
        public ICommand ImageProduct { get; set; }

        private Uri tempUri;

        private string tempIMG;
        public ICommand CloseCommand { get; set; }
        public bool CanCreate => !HasErrors;

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public EditProductViewModel(PRODUCT SelectedProduct) 
        {
            _errorsViewModel = new ErrorsViewModel();
            ProductName = SelectedProduct.PRO_NAME;
            ProductPrice = string.Format("{0:N0}", SelectedProduct.PRICE_OUT);
            ProductLink = SelectedProduct.PRO_URL;
            tempUri = new Uri(SelectedProduct.PRO_IMG);
            ProductImage = new BitmapImage(tempUri);
            tempIMG = ProductImage.ToString();
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

                decimal temp_Price = decimal.Parse(ProductPrice);
                var displaylist = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_NAME == ProductName && x.PRICE_OUT == temp_Price && x.PRO_URL == ProductLink && x.PRO_IMG == tempIMG); // nếu chưa thay đổi gì so với cái cũ thì button không được bật
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var product = DataProvider.Ins.DB.PRODUCTs.Where(x => x.PRO_ID == SelectedProduct.PRO_ID).SingleOrDefault();


                product.PRO_NAME = ProductName;
                product.PRICE_OUT = Convert.ToDecimal(ProductPrice);
                product.PRO_URL = ProductLink;
                product.PRO_IMG = ProductImage.ToString();

                DataProvider.Ins.DB.SaveChanges();

                SelectedProduct.PRO_NAME = ProductName;
                SelectedProduct.PRO_URL = ProductLink;
                SelectedProduct.PRO_IMG = ProductImage.ToString();
                SelectedProduct.PRICE_OUT = Convert.ToDecimal(ProductPrice);


                MessageBoxCustom m = new MessageBoxCustom("Cập nhật sản phẩm mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });
            
            _errorsViewModel.ErrorsChanged += _errorsViewModel_ErrorsChanged;
        }
        private void _errorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }
        public static bool IsValidUri(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
    }
}
