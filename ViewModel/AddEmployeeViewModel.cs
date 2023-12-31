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
using System.Windows.Controls;

namespace SpaManagement.ViewModel
{
    public class AddEmployeeViewModel:BaseViewModel, INotifyDataErrorInfo
    {
        public ObservableCollection<string> rolesource { get; set; }
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
                else
                if (_salary != "")
                {
                    decimal num = decimal.Parse(_salary);
                    _salary = string.Format("{0:N0}", num);

                }
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


        public ICommand AddEmployeeCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public bool CanCreate => !HasErrors;

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;


        public AddEmployeeViewModel()
        {
            rolesource = new ObservableCollection<string> { "Dịch vụ", "Quản lý", "Bảo vệ" };


            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            AddEmployeeCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(CCCD) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Salary))
                {
                    return false;
                }

                var displaylist = DataProvider.Ins.DB.EMPLOYEEs.Where(x => x.EMP_DISPLAYNAME == Name && x.EMP_CCCD == CCCD);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var employee = new EMPLOYEE() { EMP_DISPLAYNAME = Name, EMP_SALARY = decimal.Parse(Salary), EMP_ADDRESS = Address, EMP_CCCD = CCCD, EMP_PHONE = Phone, EMP_ROLE = Role };

                DataProvider.Ins.DB.EMPLOYEEs.Add(employee);
                DataProvider.Ins.DB.SaveChanges();

                EmployeeManager.AddEmployee(employee);

                MessageBoxCustom m = new MessageBoxCustom("Thêm nhân viên mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
                Name = "";
                Salary = "";
                CCCD = "";
                Phone = "";
                Role = "";
                Address = "";
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
