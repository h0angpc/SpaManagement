using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _CustomerList;
        public ObservableCollection<CUSTOMER> CustomerList
        {
            get => _CustomerList;
            set
            {
                _CustomerList = value;
                OnPropertyChanged(nameof(CustomerList));
            }
        }
        public ICommand AddCommand { get; set; }
        public CustomerViewModel()
        {
            LoadCustomerData();
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddCustomerView wd = new AddCustomerView(); wd.ShowDialog(); });
        }

        void LoadCustomerData()
        {
            //CustomerList = new ObservableCollection<CUSTOMER>();

            //var list = DataProvider.Ins.DB.CUSTOMERs;
            //foreach (var item in list)
            //{
            //    CustomerList.Add(item);
            //}
            CustomerList = new ObservableCollection<CUSTOMER>(DataProvider.Ins.DB.CUSTOMERs);
        }
    }
}
