using SpaManagement.Model;
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
    public class EditCustomerViewModel: BaseViewModel, INotifyDataErrorInfo
    {
        public ObservableCollection<string> gendersource { get; set; }
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }

        public string ID { get; set; }

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
        public ICommand EditCustomerCommand {  get; set; }
        public ICommand CloseCommand { get; set; }

        public bool CanCreate => !HasErrors;



        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public EditCustomerViewModel(CUSTOMER SelectedCus)
        {
            _errorsViewModel = new ErrorsViewModel();
            Name = SelectedCus.CUS_NAME;
            Gender = SelectedCus.CUS_SEX;
            Phone = SelectedCus.CUS_PHONE;
            Email = SelectedCus.CUS_EMAIL;
            ID = SelectedCus.CUS_MA;

            gendersource = new ObservableCollection<string> { "Nam", "Nữ" };


            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            EditCustomerCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Gender) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
                {
                    return false;
                }

                var displaylist = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_NAME == Name && x.CUS_EMAIL == Email && x.CUS_SEX == Gender &&x.CUS_PHONE == Phone);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var customer = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_MA == SelectedCus.CUS_MA).SingleOrDefault();

                customer.CUS_NAME = Name;
                customer.CUS_EMAIL = Email;
                customer.CUS_SEX = Gender;
                customer.CUS_PHONE = Phone;

                DataProvider.Ins.DB.SaveChanges();

                SelectedCus.CUS_NAME = Name;    
                SelectedCus.CUS_EMAIL = Email;
                SelectedCus.CUS_SEX = Gender;
                SelectedCus.CUS_PHONE = Phone;
            });
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
