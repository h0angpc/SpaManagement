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
    public class EditServiceViewModel : BaseViewModel,INotifyDataErrorInfo
    {
        private string _ServiceName;
        public string ServiceName { get => _ServiceName; set { _ServiceName = value; OnPropertyChanged(); } }

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
        public ICommand EditServiceCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public EditServiceViewModel(SERVICESS SelectedService)
        {
            
            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += _errorsViewModel_ErrorsChanged;

            ServiceName = SelectedService.SER_NAME;
            ServicePrice = string.Format("{0:N0}", SelectedService.PRICE);
            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            EditServiceCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(ServiceName) || string.IsNullOrEmpty(ServicePrice.ToString()))
                {
                    return false;
                }
                decimal temp_Price = Decimal.Parse(ServicePrice);
                var displaylist = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_NAME == ServiceName && x.PRICE == temp_Price); // nếu chưa thay đổi gì so với cái cũ thì button không được bật
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }
                var display = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_ID != SelectedService.SER_ID && x.SER_NAME == ServiceName);
                if(display.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var service = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_ID == SelectedService.SER_ID).SingleOrDefault();


                service.SER_NAME = ServiceName;
                service.PRICE = Convert.ToDecimal(ServicePrice);

                DataProvider.Ins.DB.SaveChanges();

                SelectedService.SER_NAME = ServiceName;
                SelectedService.PRICE = Convert.ToDecimal(ServicePrice);


                MessageBoxCustom m = new MessageBoxCustom("Cập nhật sản phẩm mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
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
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
    }
}
