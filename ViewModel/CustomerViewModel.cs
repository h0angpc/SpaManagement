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
    public class CustomerViewModel : BaseViewModel
    {
        public ObservableCollection<string> filtersource { get; set; }
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
                    CustomerCollection.Filter = FilterByName;
                }
                else if (Filtercondition == "Mã KH")
                {
                    CustomerCollection.Filter = FilterByID;
                }
            }
        }

        private ICollectionView _customerCollection;

        public ICollectionView CustomerCollection
        {
            get { return _customerCollection; }
            set 
            {
                _customerCollection = value; 
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


        public CustomerViewModel()
        {
            filtersource = new ObservableCollection<string> { "Họ tên", "Mã KH" };
            Filtercondition = "Họ tên"; // Default value

            _CustomerList = CustomerManager.GetCustomers();
            CustomerCollection = CollectionViewSource.GetDefaultView(_CustomerList);
            
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

        private bool FilterByName(object cus)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var cusDetail = cus as CUSTOMER;
                if (cusDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string customerName = cusDetail.CUS_NAME.ToLower();

                    return customerName.Contains(filtertext);
                }
            }
            return true;
        }

        private bool FilterByID(object cus)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var cusDetail = cus as CUSTOMER;
                if (cusDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string customerMA = cusDetail.CUS_MA.ToLower();

                    return customerMA.Contains(filtertext);
                }
            }
            return true;
        }
    }
}
