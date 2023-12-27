using SpaManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Controls;

namespace SpaManagement.ViewModel
{
    public class PrintBillViewModel : BaseViewModel
    {
        private ObservableCollection<Bill> _Billsource; 
        public ObservableCollection<Bill> Billsource 
        { 
            get => _Billsource;
            set 
            {
                _Billsource = value;
                OnPropertyChanged();
            } 
        }
       
        public string MaHD { get; set; }
        public string NgHD { get; set; }
        public string TongTien { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public PrintBillViewModel(PAYMENT SelectedPay)
        {
            MaHD = SelectedPay.PMT_MA;
            NgHD = SelectedPay.DAYTIME.ToString("dd/MM/yyyy HH:mm:ss");
         
            TongTien = string.Format("{0:N0} đồng", SelectedPay.PRICE);

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadBill(SelectedPay);
            });

        }
        void LoadBill(PAYMENT SelectedPay)
        {
            Billsource = new ObservableCollection<Bill>();

            var PM_Detail_Pro = new ObservableCollection<PAYMENT_DETAIL_PRODUCT>(DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PMT_ID == SelectedPay.PMT_ID));
            var PM_Detail_Ser = new ObservableCollection<PAYMENT_DETAIL_SERVICE>(DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Where(x => x.PMT_ID == SelectedPay.PMT_ID));

            foreach (var detail in PM_Detail_Pro)
            {
                if (detail != null)
                {
                    Bill ct = new Bill() {  Name = detail.PRODUCT.PRO_NAME, Price = detail.PRODUCT.PRICE, Quantity = detail.QUANTITY.ToString(), AMOUNT = detail.PRODUCT.PRICE * detail.QUANTITY };
                    Billsource.Add(ct);
                }
            }

            foreach (var detail in PM_Detail_Ser)
            {
                if (detail != null)
                {
                    Bill ct = new Bill() {  Name = detail.SERVICESS.SER_NAME, Price = detail.SERVICESS.PRICE, Quantity = detail.QUANTITY.ToString(), AMOUNT = detail.SERVICESS.PRICE * detail.QUANTITY };
                    Billsource.Add(ct);
                }
            }
        }

    }
}
