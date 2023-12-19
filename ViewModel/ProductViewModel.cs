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
        private ObservableCollection<PRODUCT> _ProductList;
        public ObservableCollection<PRODUCT> ProductList { get => _ProductList; set { _ProductList = value; OnPropertyChanged(); } }
        public ICommand ShowAddProCommand { get; set; }
        public ICommand ShowEditProCommand { get; set; }

        private ICollectionView _productcollection;

        public ICollectionView ProductCollection
        {
            get { return _productcollection; }
            set {  _productcollection = value; OnPropertyChanged(); }
        }

        private PRODUCT _SelectedItem;
        public PRODUCT SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {


                }
            }
        }


        public ProductViewModel()
        {
            ShowAddProCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddProductView wd = new AddProductView(); wd.ShowDialog(); });
            //_ProductList = ProductManager.GetProducts();
            //ProductCollection = CollectionViewSource.GetDefaultView(_ProductList); 
            ProductList = new ObservableCollection<PRODUCT>(DataProvider.Ins.DB.PRODUCTs);
        }
        
        
    }
}
