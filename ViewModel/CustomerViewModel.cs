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
                OnPropertyChanged();
            }
        }
        public ICommand ShowAddCusCommand { get; set; }
        public ICommand ShowEditCusCommand { get; set; }
        public CustomerViewModel()
        {
            _CustomerList = CustomerManager.GetCustomers();
            ShowAddCusCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddCustomerView wd = new AddCustomerView(); wd.ShowDialog();});
            ShowEditCusCommand = new RelayCommand<CUSTOMER>((p) => { return p!=null; }, (p) => { 
                if (p !=null)
                {
                    EditCustomerViewModel editViewModel = new EditCustomerViewModel(p);
                    EditCustomerView editView = new EditCustomerView();
                    editView.DataContext = editViewModel;
                    editView.ShowDialog();
                }
            });
        }
    }
}
