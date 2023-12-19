using SpaManagement.Commands;
using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class AddCustomerViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        public ObservableCollection<string> gendersource { get; set; }
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _email;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;

                _errorsViewModel.ClearErrors(nameof(Phone));
                if (!IsNumeric(_phone) && _phone != "")
                {
                    _errorsViewModel.AddError(nameof(Phone), "Số điện thoại chỉ có các con số");
                }

                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        public ICommand AddCustomerCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public bool CanCreate => !HasErrors;

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;


        public AddCustomerViewModel() 
        {
            gendersource = new ObservableCollection<string> { "Nam", "Nữ" };


            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            AddCustomerCommand = new RelayCommand<Window>((p) => 
            { 
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Phone))
                {
                    return false;
                }

                var displaylist = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_NAME == Name && x.CUS_EMAIL == Email && x.CUS_SEX == Gender);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) => 
            {
                var customer = new CUSTOMER() {CUS_NAME = Name, CUS_EMAIL = Email, CUS_SEX = Gender, CUS_PHONE = Phone};

                DataProvider.Ins.DB.CUSTOMERs.Add(customer);
                DataProvider.Ins.DB.SaveChanges();

                CustomerManager.AddCustomer(customer);

                MessageBoxCustom m = new MessageBoxCustom("Thêm khách hàng mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });


            _errorsViewModel = new ErrorsViewModel();
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
    }
}
