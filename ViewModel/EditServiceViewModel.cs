

using Microsoft.Win32;
using SpaManagement.Model;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Security.Policy;
using System.Xml.Linq;
using SpaManagement.Views;

namespace SpaManagement.ViewModel
{
    public class EditServiceViewModel : BaseViewModel
    {
        private string _ServiceName;
        public string ServiceName { get => _ServiceName; set { _ServiceName = value; OnPropertyChanged(); } }

        private decimal _ServicePrice;
        public decimal ServicePrice { get => _ServicePrice; set { _ServicePrice = value; OnPropertyChanged(); } }
        public ICommand EditServiceCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public EditServiceViewModel(SERVICESS SelectedService)
        {
            ServiceName = SelectedService.SER_NAME;
            ServicePrice = SelectedService.PRICE;
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

                var displaylist = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_NAME == ServiceName && x.PRICE == ServicePrice); // nếu chưa thay đổi gì so với cái cũ thì button không được bật
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var service = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_ID == SelectedService.SER_ID).SingleOrDefault();


                service.SER_NAME = ServiceName;
                service.PRICE = ServicePrice;

                DataProvider.Ins.DB.SaveChanges();

                SelectedService.SER_NAME = ServiceName;
                SelectedService.PRICE = ServicePrice;


                MessageBoxCustom m = new MessageBoxCustom("Cập nhật sản phẩm mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });

        }

    }
}
