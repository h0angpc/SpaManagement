using Microsoft.Win32;
using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AddServiceViewModel : BaseViewModel
    {
        private string _ServiceName;
        public string ServiceName { get => _ServiceName; set { _ServiceName = value; OnPropertyChanged(); } }

        private string _ServicePrice;
        public string ServicePrice { get => _ServicePrice; set { _ServicePrice = value; OnPropertyChanged(); } }

       
        public ICommand AddServiceCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public AddServiceViewModel()
        {
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
                var displaylist = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_NAME == ServiceName);
                if (displaylist == null || displaylist.Count() != 0)
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
                ServicePrice = null;
            });


        }
    }
}
