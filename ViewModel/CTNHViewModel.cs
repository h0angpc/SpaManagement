using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class CTNHViewModel:BaseViewModel
    {
        private ObservableCollection<RECEIPT_DETAIL> _CTNHsource;
        public ObservableCollection<RECEIPT_DETAIL> CTNHsource
        {
            get => _CTNHsource;
            set
            {
                _CTNHsource = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand RemoveReceiptCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public string MaNH { get; set; }
        public string NgNH { get; set; }
        public string TongTien { get; set; }
        public CTNHViewModel(RECEIPT SelectedRec) 
        {
            MaNH = SelectedRec.REC_MA;
            NgNH = SelectedRec.REC_DATE.ToString("dd/MM/yyyy HH:mm:ss");
            TongTien = string.Format("{0:N0} đồng", SelectedRec.REC_PRICE);

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                LoadCTNH(SelectedRec);
            }
            );

            RemoveReceiptCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                bool? result = new MessageBoxCustom("Xác nhận xóa thông tin nhập hàng?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    //var PM_Detail_Pro = new ObservableCollection<PAYMENT_DETAIL_PRODUCT>(DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PMT_ID == SelectedPay.PMT_ID));
                    //var PM_Detail_Ser = new ObservableCollection<PAYMENT_DETAIL_SERVICE>(DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Where(x => x.PMT_ID == SelectedPay.PMT_ID));

                    //foreach (var ct in PM_Detail_Pro)
                    //{
                    //    DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Remove(ct);
                    //}
                    //foreach (var ct in PM_Detail_Ser)
                    //{
                    //    DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Remove(ct);
                    //}

                    //PaymentManager.RemovePayment(SelectedPay);
                    //DataProvider.Ins.DB.PAYMENTs.Remove(SelectedPay);

                    foreach (var ct in CTNHsource)
                    {
                        DataProvider.Ins.DB.RECEIPT_DETAIL.Remove(ct);
                    }

                    ReceiptManager.RemoveReceipt(SelectedRec);
                    DataProvider.Ins.DB.RECEIPTs.Remove(SelectedRec);

                    DataProvider.Ins.DB.SaveChanges();
                    p.Close();
                    MessageBoxCustom m = new MessageBoxCustom("Xóa thông tin nhập hàng thành công!", MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                }
                else
                {
                    return;
                }
            }
            );
        }

        void LoadCTNH(RECEIPT SelectedReceipt)
        {
            CTNHsource = new ObservableCollection<RECEIPT_DETAIL>(DataProvider.Ins.DB.RECEIPT_DETAIL.Where(x => x.REC_ID == SelectedReceipt.REC_ID));
        }
    }
}
