using Microsoft.Win32;
using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
namespace SpaManagement.ViewModel
{
    public class AddServiceViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private string _ServiceName;
        public string ServiceName
        {
            get => _ServiceName;
            set
            {
                _ServiceName = value;
                _errorsViewModel.ClearErrors(nameof(ServiceName));
                if (!CheckIfServiceExist(_ServiceName) && _ServiceName != "")
                {
                    _errorsViewModel.AddError(nameof(ServiceName), "Dịch vụ đã tồn tại");
                }

                OnPropertyChanged(nameof(ServiceName));
            }
        }

        private string _ServicePrice;
        public string ServicePrice
        {
            get
            {
                return _ServicePrice;
            }
            set
            {
                _ServicePrice = value;

                _errorsViewModel.ClearErrors(nameof(ServicePrice));
                if (!IsNumeric(_ServicePrice.Replace(",", "")) && _ServicePrice != "")
                {
                    _errorsViewModel.AddError(nameof(ServicePrice), "Giá tiền không được chứa chữ cái");
                }
                else
                if (_ServicePrice != "")
                {
                    decimal num = decimal.Parse(_ServicePrice);
                    _ServicePrice = string.Format("{0:N0}", num);
                }


                OnPropertyChanged(nameof(ServicePrice));
            }
        }
        public bool CanCreate => !HasErrors;

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public ICommand AddServiceCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public AddServiceViewModel()
        {
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += _errorsViewModel_ErrorsChanged;
            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });
            AddServiceCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ServiceName) || string.IsNullOrEmpty(ServicePrice))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var Service = new SERVICESS() { SER_NAME = ServiceName, PRICE = Convert.ToDecimal(ServicePrice) };

                DataProvider.Ins.DB.SERVICESSes.Add(Service);
                DataProvider.Ins.DB.SaveChanges();

                ServiceManager.AddServcie(Service);

                MessageBoxCustom m = new MessageBoxCustom("Thêm dịch vụ mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
                ServiceName = null;
                ServicePrice = "";
            });


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
       

        public bool CheckIfServiceExist(string ServiceName)
        {
            var displaylist = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_NAME == ServiceName);
            if (displaylist == null || displaylist.Count() != 0)
            {
                return false;
            }
            return true;
        }
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
    }
}
