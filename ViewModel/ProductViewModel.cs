using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SpaManagement.Model;
using SpaManagement.Views;
namespace SpaManagement.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }

        private decimal _ProductPrice;
        public decimal ProductPrice { get => _ProductPrice; set { _ProductPrice = value; OnPropertyChanged(); } }

        private string _ProductLink;
        public string ProductLink { get => _ProductLink; set { _ProductLink = value; OnPropertyChanged(); } }

        private BitmapImage _ProductImage;
        public BitmapImage ProductImage { get => _ProductImage; set { _ProductImage = value; OnPropertyChanged(); } }

        private ObservableCollection<PRODUCT> _ProductList;
        public ObservableCollection<PRODUCT> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }
        public ICommand ShowAddProCommand { get; set; }
        public ICommand ShowEditProCommand { get; set; }
        public ICommand LinkButtonCommand { get; set; }
        public ICommand RemoveProCommand { get; set; }

        private string _textToFilter;

        
        public string TextToFilter
        {
            get { return _textToFilter; }
            set { _textToFilter = value;
                OnPropertyChanged(nameof(TextToFilter));
                ProductCollection.Filter = FilterByName;
            }
        }


        private string ImagePath;

        private ICollectionView _productcollection;
        public ICollectionView ProductCollection
        {
            get { return _productcollection; }
            set {  _productcollection = value; OnPropertyChanged(); }
        }

        private PRODUCT _SelectedProduct;
        public PRODUCT SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
            }
        }


        public ProductViewModel()
        {
            ShowAddProCommand = new RelayCommand<object>((p) => 
            { 
                return true;
            }, (p) => 
            { 
                AddProductView wd = new AddProductView();
                wd.ShowDialog();
            });
            _ProductList = ProductManager.GetProducts();
            ProductCollection = CollectionViewSource.GetDefaultView(_ProductList);

            LinkButtonCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedProduct == null)
                    return false;
                return true;
            },
            (p) =>
            {
                try
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = SelectedProduct.PRO_URL,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening URL: " + ex.Message);
                }


            });

            ShowEditProCommand = new RelayCommand<PRODUCT>((p) =>
            {
                if(SelectedProduct == null)
                    return false;
                return true;
            },
            (p) =>
            {
                EditProductViewModel editProductViewModel = new EditProductViewModel(SelectedProduct);
                EditProductView wd = new EditProductView();
                wd.DataContext = editProductViewModel;
                wd.ShowDialog();

            });

            RemoveProCommand = new RelayCommand<PRODUCT>((p) =>
            {
                if (SelectedProduct == null)
                    return false;
                return true;
            },
            (p) =>
            {
                try
                {
                    if (p !=null)
                    {
                        PAYMENT_DETAIL_PRODUCT pay = DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.FirstOrDefault(x => x.P_ID == p.PRO_ID);

                        if (pay!=null)
                        {
                            MessageBoxCustom m = new MessageBoxCustom("Không thể xóa sản phẩm này vì tồn tại nhiều dữ liệu liên quan", MessageType.Info, MessageButtons.Ok);
                            m.ShowDialog();
                        }
                        else
                        {
                            bool? result = new MessageBoxCustom("Xác nhận xóa sản phẩm?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                            if (result.Value)
                            {
                                ProductManager.RemoveProduct(p);
                                DataProvider.Ins.DB.PRODUCTs.Remove(p);

                                DataProvider.Ins.DB.SaveChanges();
                                MessageBoxCustom m = new MessageBoxCustom("Xóa sản phẩm thành công!", MessageType.Info, MessageButtons.Ok);
                                m.ShowDialog();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxCustom m = new MessageBoxCustom(ex.Message, MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                }

            });
        }
        private bool FilterByName(object pro)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var proDetail = pro as PRODUCT;
                if (proDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string productName = proDetail.PRO_NAME.ToLower();

                    return productName.Contains(filtertext);
                }
            }
            return true;
        }


    }
}
