using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class AddPaymentViewModel: BaseViewModel
    {
        public bool IsConfirm;
        private ObservableCollection<string> _sersource;
        public ObservableCollection<string> sersource
        {
            get => _sersource;
            set
            {
                _sersource = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PAYMENT_DETAIL_PRODUCT> _PM_Detail_Pro;
        public ObservableCollection<PAYMENT_DETAIL_PRODUCT> PM_Detail_Pro
        {
            get => _PM_Detail_Pro;
            set
            {
                _PM_Detail_Pro = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PAYMENT_DETAIL_SERVICE> _PM_Detail_Ser;
        public ObservableCollection<PAYMENT_DETAIL_SERVICE> PM_Detail_Ser
        {
            get => _PM_Detail_Ser;
            set
            {
                _PM_Detail_Ser = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _prosource;
        public ObservableCollection<string> prosource
        {
            get => _prosource;
            set
            {
                _prosource = value;
                OnPropertyChanged();
            }
        }
        public string Ma_Ten_KH { get; set; }
        public string Ma_HD { get; set; }
        public int MA_Pro { get; set; }
        public decimal price_pro { get; set; }


        private string _SelectedProduct;

        public string SelectedProduct
        {
            get => _SelectedProduct; 
            set
            {
                _SelectedProduct = value;
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(SelectedProduct))
                {
                    string productId = SelectedProduct.Split('|')[0].Trim();

                    var selectedPrice = DataProvider.Ins.DB.PRODUCTs.FirstOrDefault(x => x.PRO_MA == productId);
                    MA_Pro = selectedPrice.PRO_ID;

                    if (selectedPrice != null)
                    {
                        price_pro  = selectedPrice.PRICE;
                        PricePro = string.Format("{0:N0}", price_pro);
                    }
                }
            }
        }


        private string _PricePro;

        public string PricePro
        {
            get => _PricePro;
            set 
            {
                _PricePro = value;
                OnPropertyChanged();
            }
        }

        private int _ProQuantity;

        public int ProQuantity
        {
            get => _ProQuantity;
            set 
            {
                _ProQuantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalPrice;

        public decimal TotalPrice
        {
            get => _TotalPrice; 
            set 
            { 
                _TotalPrice = value;
                OnPropertyChanged();
            }
        }



        public ICommand AddProDetailCommand { get; set; }
        public ICommand RemoveProDetailCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        public AddPaymentViewModel(PAYMENT payment, string SelectedCus) 
        {
            TotalPrice = 0;
            IsConfirm = false;

            prosource = new ObservableCollection<string>(DataProvider.Ins.DB.PRODUCTs.Select(x => x.PRO_MA + " | " + x.PRO_NAME).ToList());
            sersource = new ObservableCollection<string>(DataProvider.Ins.DB.SERVICESSes.Select(x => x.SER_MA + " | " + x.SER_NAME).ToList());

            Ma_Ten_KH = SelectedCus;
            Ma_HD = payment.PMT_MA;

            PM_Detail_Pro = new ObservableCollection<PAYMENT_DETAIL_PRODUCT>(DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PMT_ID == payment.PMT_ID));

            PM_Detail_Ser = new ObservableCollection<PAYMENT_DETAIL_SERVICE>(DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.Where(x => x.PMT_ID == payment.PMT_ID));

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    if (!IsConfirm)
                    {
                        bool? result = new MessageBoxCustom("Hóa đơn chưa tạo! Xác nhận thoát?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                        if (result.Value)
                        {
                            DataProvider.Ins.DB.PAYMENTs.Remove(payment);
                            DataProvider.Ins.DB.SaveChanges();
                            w.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        w.Close();
                    }
                }
            });

            AddProDetailCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(SelectedProduct) || ProQuantity == 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var detail = PM_Detail_Pro.Where(x => x.PMT_ID == payment.PMT_ID && x.P_ID == MA_Pro).SingleOrDefault();
                if (detail == null)
                {
                    decimal sumprice = ProQuantity * price_pro;

                    var prodetail = new PAYMENT_DETAIL_PRODUCT() { PMT_ID = payment.PMT_ID, P_ID = MA_Pro, QUANTITY = ProQuantity, AMOUNT = sumprice };

                    DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Add(prodetail);

                    PM_Detail_Pro.Add(prodetail);

                    TotalPrice += prodetail.AMOUNT;
                }
                else
                {
                    detail.QUANTITY += ProQuantity;
                    detail.AMOUNT += ProQuantity * price_pro;

                    TotalPrice += ProQuantity * price_pro;
                }

            });

            RemoveProDetailCommand = new RelayCommand<PAYMENT_DETAIL_PRODUCT>((p) =>
            {
                if (p == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                TotalPrice -= p.AMOUNT;
                DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Remove(p);
                PM_Detail_Pro.Remove(p);
            });

            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                if (PM_Detail_Pro.Count == 0 && PM_Detail_Ser.Count == 0)
                    return false;
                return true;
            }, (p) =>
            {
                bool? result = new MessageBoxCustom("Xác nhận tạo hóa đơn?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    IsConfirm = true;
                    payment.PRICE = TotalPrice;
                    PaymentManager.AddPayment(payment);
                    foreach (var prodetail in PM_Detail_Pro)
                    {
                        DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Add(prodetail);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBoxCustom m = new MessageBoxCustom("Tạo hóa đơn thành công!", MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                    p.Close();
                }
                else
                {
                    return;
                }
            });
        }
    }
}
