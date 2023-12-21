using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        
        public ObservableCollection<string> filtersource { get; set; }
        private string _TextToFilter;

        public string TextToFilter
        {
            get { return _TextToFilter; }
            set
            {
                _TextToFilter = value;
                OnPropertyChanged(nameof(TextToFilter));
                if (Filtercondition == "Họ tên")
                {
                    PaymentCollection.Filter = FilterByName;
                }
                else if (Filtercondition == "Ngày")
                {
                    PaymentCollection.Filter = FilterByDate;
                }
                else if (Filtercondition == "Số HD")
                {
                    PaymentCollection.Filter = FilterByPMTMA;
                }    
            }
        }

        private string _filtercondition;
        public string Filtercondition
        {
            get
            {
                return _filtercondition;
            }
            set
            {
                _filtercondition = value;
                OnPropertyChanged(nameof(Filtercondition));
            }
        }
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
        public ICommand ShowAddPayCommand { get; set; }
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
            filtersource = new ObservableCollection<string> { "Họ tên","Số HD", "Ngày" };
            Filtercondition = "Họ tên"; // Default value
            _CustomerList = CustomerManager.GetCustomers();
            _PaymentList = PaymentManager.GetPayment();
            PaymentCollection = CollectionViewSource.GetDefaultView(_PaymentList);
            ShowAddPayCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddCustomerView wd = new AddCustomerView(); wd.ShowDialog(); });
            ShowCTHDCommand = new RelayCommand<PAYMENT>((p) => { return p != null; }, (p) =>
            {
                if (p != null)
                {
               
                    CTHDView CTView = new CTHDView();
               
                 
                }

            });
            }
        private bool FilterByName(object pay)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var payDetail = pay as PAYMENT;
                if (payDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string customerName = payDetail.CUSTOMER.CUS_NAME.ToLower();

                    return customerName.Contains(filtertext);
                }
            }
            return true;
        }
        private bool FilterByPMTMA(object MA)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var payDetail = MA as PAYMENT;
                if (payDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string paymentMA = payDetail.PMT_MA.ToLower();

                    return paymentMA.Contains(filtertext);
                }
            }
            return true;
        }

        private bool FilterByDate(object pay)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var payDetail = pay as PAYMENT;
                if (payDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string date = payDetail.DAYTIME.ToString("dd/MM/yyyy HH:mm:ss");

                    return date.Contains(filtertext);
                }
            }
            return true;
        }
    }
}
