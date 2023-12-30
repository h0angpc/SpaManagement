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
        public ICommand RemoveCusCommand { get; set; }

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

            RemoveCusCommand = new RelayCommand<CUSTOMER>((p) => { return p!=null; }, (p) => {
                try
                {
                    if (p !=null)
                    {
                        PAYMENT pay = DataProvider.Ins.DB.PAYMENTs.FirstOrDefault(x => x.CUSTOMER.CUS_ID == p.CUS_ID);
                        BOOKING book = DataProvider.Ins.DB.BOOKINGs.FirstOrDefault(x => x.CUSTOMER.CUS_ID == p.CUS_ID);

                        if (pay!=null || book !=null)
                        {
                            MessageBoxCustom m = new MessageBoxCustom("Không thể xóa khách hàng này vì tồn tại nhiều dữ liệu liên quan", MessageType.Info, MessageButtons.Ok);
                            m.ShowDialog();
                        }
                        else
                        {
                            bool? result = new MessageBoxCustom("Xác nhận xóa khách hàng?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                            if (result.Value)
                            {
                                CustomerManager.RemoveCustomer(p);
                                DataProvider.Ins.DB.CUSTOMERs.Remove(p);

                                DataProvider.Ins.DB.SaveChanges();
                                MessageBoxCustom m = new MessageBoxCustom("Xóa khách hàng thành công!", MessageType.Info, MessageButtons.Ok);
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
