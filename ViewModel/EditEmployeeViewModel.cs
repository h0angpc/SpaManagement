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
using System.Windows.Input;
using System.Windows;

namespace SpaManagement.ViewModel
{
    public class EditEmployeeViewModel:BaseViewModel, INotifyDataErrorInfo
    {
        public ObservableCollection<string> rolesource { get; set; }
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

        private string _address;

        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
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

        private string _role;
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }


        private string _salary;

        public string Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;

                _errorsViewModel.ClearErrors(nameof(Salary));
                if (!IsNumeric(_salary.Replace(",", "")) && _salary != "")
                {
                    _errorsViewModel.AddError(nameof(Salary), "Lương nhân viên chỉ có các con số");
                }

                decimal num = decimal.Parse(_salary);
                _salary = string.Format("{0:N0}", num);


                OnPropertyChanged(nameof(Salary));
            }
        }

        private string _cccd;
        public string CCCD
        {
            get
            {
                return _cccd;
            }
            set
            {
                _cccd = value;

                _errorsViewModel.ClearErrors(nameof(CCCD));
                if (!IsNumeric(_cccd) && _cccd != "")
                {
                    _errorsViewModel.AddError(nameof(CCCD), "Số căn cước chỉ có các con số");
                }

                OnPropertyChanged(nameof(CCCD));
            }
        }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public bool CanCreate => !HasErrors;



        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public EditEmployeeViewModel(EMPLOYEE SelectedEmp)
        {
            _errorsViewModel = new ErrorsViewModel();

            ID = SelectedEmp.EMP_MA;
            Name = SelectedEmp.EMP_DISPLAYNAME;
            Role = SelectedEmp.EMP_ROLE;
            Phone = SelectedEmp.EMP_PHONE;
            Address = SelectedEmp.EMP_ADDRESS;
            CCCD = SelectedEmp.EMP_CCCD;
            Salary = string.Format("{0:N0}", SelectedEmp.EMP_SALARY);

            rolesource = new ObservableCollection<string> { "Dịch vụ", "Quản lý", "Bảo vệ" };

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            EditEmployeeCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(CCCD) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Salary))
                {
                    return false;
                }

                decimal luong_tam = decimal.Parse(Salary);

                var displaylist = DataProvider.Ins.DB.EMPLOYEEs.Where(x => x.EMP_DISPLAYNAME == Name && x.EMP_CCCD == CCCD && x.EMP_SALARY == luong_tam && x.EMP_PHONE == Phone && x.EMP_ROLE == Role && x.EMP_ADDRESS == Address);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                //var customer = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_MA == SelectedCus.CUS_MA).SingleOrDefault();

                //customer.CUS_NAME = Name;
                //customer.CUS_EMAIL = Email;
                //customer.CUS_SEX = Gender;
                //customer.CUS_PHONE = Phone;
                var employee = DataProvider.Ins.DB.EMPLOYEEs.Where(x => x.EMP_MA == SelectedEmp.EMP_MA).SingleOrDefault();

                employee.EMP_DISPLAYNAME = Name;
                employee.EMP_PHONE = Phone;
                employee.EMP_ROLE = Role;
                employee.EMP_ADDRESS = Address;
                employee.EMP_SALARY = decimal.Parse(Salary);
                employee.EMP_CCCD = CCCD;

                DataProvider.Ins.DB.SaveChanges();

                SelectedEmp.EMP_DISPLAYNAME = Name;
                SelectedEmp.EMP_PHONE = Phone;
                SelectedEmp.EMP_ROLE = Role;
                SelectedEmp.EMP_ADDRESS = Address;
                SelectedEmp.EMP_SALARY = decimal.Parse(Salary);
                SelectedEmp.EMP_CCCD = CCCD;

                MessageBoxCustom m = new MessageBoxCustom("Cập nhật thành công!", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();

                //DataProvider.Ins.DB.SaveChanges();

                //SelectedCus.CUS_NAME = Name;
                //SelectedCus.CUS_EMAIL = Email;
                //SelectedCus.CUS_SEX = Gender;
                //SelectedCus.CUS_PHONE = Phone;
                //MessageBoxCustom m = new MessageBoxCustom("Cập nhật thành công!", MessageType.Info, MessageButtons.Ok);
                //m.ShowDialog();
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
