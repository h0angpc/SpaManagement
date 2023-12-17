using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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



        public ProductViewModel()
        {
            ShowAddProCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddProductView wd = new AddProductView(); wd.ShowDialog(); });
            //ProductList = new ObservableCollection<PRODUCT>(DataProvider.Ins.DB.PRODUCTs);
        }
        
        
    }
}
