using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class PaymentViewModel : BaseViewModel
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
        public ICommand ShowAddPayCommand { get; set; }
        private ObservableCollection<PAYMENT> _PaymentList;
        public ObservableCollection<PAYMENT> PaymentList
        {
            get => _PaymentList;
            set
            {
                _PaymentList = value;
                OnPropertyChanged();
            }
        }
        public ICommand ShowCTHDCommand { get; set; }

        private ICollectionView _paymentCollection;
        public ICollectionView PaymentCollection
        {
            get { return _paymentCollection; }
            set
            {
                _paymentCollection = value;
            }
        }
        public PaymentViewModel()
        {
            _CustomerList = CustomerManager.GetCustomers();
            _PaymentList = PaymentManager.GetPayment();
            PaymentCollection = CollectionViewSource.GetDefaultView( _PaymentList);
            ShowAddPayCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddCustomerView wd = new AddCustomerView(); wd.ShowDialog(); });

        }
    }
}
