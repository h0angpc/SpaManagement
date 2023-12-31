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
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class CTHDViewModel : BaseViewModel
    {
        private ObservableCollection<CTHD> _CTHDsource;
        public ObservableCollection<CTHD> CTHDsource
        {
            get => _CTHDsource;
            set 
            {
                _CTHDsource = value;
                OnPropertyChanged();
            }
        }

        

        public string MaHD { get; set; }
        public string NgHD { get; set; }
        public string TenKH { get; set; }
        public string TongTien { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand RemovePaymentCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public CTHDViewModel(PAYMENT SelectedPay) 
        {
            MaHD = SelectedPay.PMT_MA;
            NgHD = SelectedPay.DAYTIME.ToString("dd/MM/yyyy HH:mm:ss");
            TenKH = SelectedPay.CUSTOMER.CUS_NAME;
            TongTien = string.Format("{0:N0} đồng", SelectedPay.PRICE);

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            RemovePaymentCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                bool? result = new MessageBoxCustom("Xác nhận xóa hóa đơn?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    var PM_Detail_Pro = new ObservableCollection<PAYMENT_DETAIL_PRODUCT>(DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PMT_ID == SelectedPay.PMT_ID));
                    var PM_Detail_Ser = new ObservableCollection<PAYMENT_DETAIL_SERVICE>(DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Where(x => x.PMT_ID == SelectedPay.PMT_ID));

                    foreach (var ct in PM_Detail_Pro)
                    {
                        DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Remove(ct);
                    }
                    foreach (var ct in PM_Detail_Ser)
                    {
                        DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Remove(ct);
                    }

                    PaymentManager.RemovePayment(SelectedPay);
                    DataProvider.Ins.DB.PAYMENTs.Remove(SelectedPay);

                    DataProvider.Ins.DB.SaveChanges();
                    p.Close();
                    MessageBoxCustom m = new MessageBoxCustom("Xóa hóa đơn thành công!", MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                }
                else
                {
                    return;
                }
            }
            );

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                LoadCTHD(SelectedPay);
            }
            );
        }

        void LoadCTHD(PAYMENT SelectedPay)
        {
            CTHDsource = new ObservableCollection<CTHD>();

            var PM_Detail_Pro = new ObservableCollection<PAYMENT_DETAIL_PRODUCT>(DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PMT_ID == SelectedPay.PMT_ID));
            var PM_Detail_Ser = new ObservableCollection<PAYMENT_DETAIL_SERVICE>(DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Where(x => x.PMT_ID == SelectedPay.PMT_ID));

            foreach (var detail in PM_Detail_Pro)
            {
                if (detail != null)
                {
                    CTHD ct = new CTHD() { ID = detail.PRODUCT.PRO_MA, Name = detail.PRODUCT.PRO_NAME, Price = detail.PRICE , Quantity = detail.QUANTITY.ToString(), AMOUNT = detail.PRICE * detail.QUANTITY };
                    CTHDsource.Add(ct);
                }
            }

            foreach (var detail in PM_Detail_Ser)
            {
                if (detail != null)
                {
                    CTHD ct = new CTHD() { ID = detail.SERVICESS.SER_MA, Name = detail.SERVICESS.SER_NAME, Price = detail.PRICE , Quantity = detail.QUANTITY.ToString(), AMOUNT = detail.PRICE * detail.QUANTITY };
                    CTHDsource.Add(ct);
                }
            }
        }
    }
}
