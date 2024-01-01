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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class AddReceiptViewModel:BaseViewModel,INotifyDataErrorInfo
    {
        public bool IsConfirm;
        public bool IsNumeric(string value)
        {
            return long.TryParse(value, out _);
        }
        public string Ma_NH {  get; set; }
        public int Pro_ID { get; set; }
        public ICommand AddDetailCommand { get; set; }
        public ICommand RemoveDetailCommand { get; set; }
        public ICommand EditDetailCommand {  get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private ObservableCollection<RECEIPT_DETAIL> _RC_Detail;
        public ObservableCollection<RECEIPT_DETAIL> RC_Detail
        {
            get => _RC_Detail;
            set
            {
                _RC_Detail = value;
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
                    string productMA = SelectedProduct.Split('|')[0].Trim();

                    var selectedPro = DataProvider.Ins.DB.PRODUCTs.FirstOrDefault(x => x.PRO_MA == productMA);
                    Pro_ID = selectedPro.PRO_ID;

                }
            }
        }

        private string _PriceIn;

        public string PriceIn
        {
            get
            {
                return _PriceIn;
            }
            set
            {
                _PriceIn = value;

                _errorsViewModel.ClearErrors(nameof(PriceIn));
                if (!IsNumeric(_PriceIn.Replace(",", "")) && _PriceIn != "")
                {
                    _errorsViewModel.AddError(nameof(PriceIn), "Giá tiền không hợp lệ");
                }
                else
                if (_PriceIn != "")
                {
                    decimal num = decimal.Parse(_PriceIn);
                    _PriceIn = string.Format("{0:N0}", num);

                }
                OnPropertyChanged(nameof(PriceIn));
            }
        }

        private string _ProQuantity;

        public string ProQuantity
        {
            get => _ProQuantity;
            set
            {
                _ProQuantity = value;

                _errorsViewModel.ClearErrors(nameof(ProQuantity));
                if ((!IsNumeric(_ProQuantity) || int.Parse(_ProQuantity) <= 0) && _ProQuantity != "")
                {
                    _errorsViewModel.AddError(nameof(ProQuantity), "Số lượng không hợp lệ");
                }

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

        private RECEIPT_DETAIL _SelectedDetail;

        public RECEIPT_DETAIL SelectedDetail
        {
            get 
            {
                return _SelectedDetail; 
            }
            set
            {
                _SelectedDetail = value; 
                OnPropertyChanged(); 
                if (SelectedDetail != null)
                {
                    SelectedProduct = SelectedDetail.PRODUCT.PRO_MA + " | " + SelectedDetail.PRODUCT.PRO_NAME;
                    PriceIn  = SelectedDetail.PRICE.ToString();
                    ProQuantity = SelectedDetail.QUANTITY.ToString();
                }
            }
        }


        public bool CanCreate => !HasErrors;

        private readonly ErrorsViewModel _errorsViewModel;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errorsViewModel.HasErrors;
        public AddReceiptViewModel(RECEIPT receipt) 
        {
            IsConfirm = false;
            TotalPrice = 0;

            Ma_NH = receipt.REC_MA;

            prosource = new ObservableCollection<string>(DataProvider.Ins.DB.PRODUCTs.Select(x => x.PRO_MA + " | " + x.PRO_NAME).ToList());

            RC_Detail = new ObservableCollection<RECEIPT_DETAIL>(DataProvider.Ins.DB.RECEIPT_DETAIL.Where(x => x.REC_ID == receipt.REC_ID));

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    if (!IsConfirm)
                    {
                        bool? result = new MessageBoxCustom("Chưa nhập hàng! Xác nhận thoát?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                        if (result.Value)
                        {
                            DataProvider.Ins.DB.RECEIPTs.Remove(receipt);
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

            AddDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SelectedProduct) ||  string.IsNullOrEmpty(ProQuantity) || decimal.Parse(PriceIn) == 0)
                {
                    return false;
                }

                var detail = RC_Detail.Where(x => x.REC_ID == receipt.REC_ID && x.P_ID == Pro_ID).SingleOrDefault();

                if (detail != null)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var detail = RC_Detail.Where(x => x.REC_ID == receipt.REC_ID && x.P_ID == Pro_ID).SingleOrDefault();

                decimal sumprice = int.Parse(ProQuantity) * decimal.Parse(PriceIn);

                var prodetail = new RECEIPT_DETAIL() { REC_ID = receipt.REC_ID, P_ID = Pro_ID, QUANTITY = int.Parse(ProQuantity), PRICE = decimal.Parse(PriceIn), AMOUNT = sumprice };

                DataProvider.Ins.DB.RECEIPT_DETAIL.Add(prodetail);

                RC_Detail.Add(prodetail);

                TotalPrice += prodetail.AMOUNT;

                SelectedProduct = null;
                PriceIn = "";
                ProQuantity = "";
            });

            RemoveDetailCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDetail == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                TotalPrice -= SelectedDetail.AMOUNT;
                DataProvider.Ins.DB.RECEIPT_DETAIL.Remove(SelectedDetail);
                RC_Detail.Remove(SelectedDetail);

                PriceIn = "";
                ProQuantity = "";
                SelectedProduct = null;
            });

            EditDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(ProQuantity) || int.Parse(ProQuantity) == 0  || !IsNumeric(ProQuantity) || decimal.Parse(PriceIn) == 0)
                {
                    return false;
                }
                if (SelectedDetail == null)
                {
                    return false;
                }
                if (SelectedDetail != null && SelectedDetail.P_ID != Pro_ID)
                {
                    return false;
                }
                
                if (SelectedDetail.PRICE == decimal.Parse(PriceIn) && SelectedDetail.QUANTITY == int.Parse(ProQuantity))
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                TotalPrice -= SelectedDetail.AMOUNT;
                var detail = RC_Detail.Where(x => x.REC_ID == SelectedDetail.REC_ID && x.P_ID == SelectedDetail.P_ID).SingleOrDefault();

                detail.QUANTITY = int.Parse(ProQuantity);
                detail.PRICE = decimal.Parse(PriceIn);
                detail.AMOUNT = detail.QUANTITY * detail.PRICE;

                SelectedDetail.QUANTITY = int.Parse(ProQuantity);
                SelectedDetail.PRICE = decimal.Parse(PriceIn);
                SelectedDetail.AMOUNT = SelectedDetail.QUANTITY * SelectedDetail.PRICE;
                TotalPrice += SelectedDetail.AMOUNT;

                PriceIn = "";
                ProQuantity = "";
                SelectedProduct = null;
                SelectedDetail = null;
            });

            ConfirmCommand = new RelayCommand<Window>((p) =>
            {
                if (RC_Detail.Count == 0)
                    return false;
                return true;
            }, (p) =>
            {
                bool? result = new MessageBoxCustom("Xác nhận nhập hàng?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    IsConfirm = true;
                    receipt.REC_PRICE = TotalPrice;
                    ReceiptManager.AddReceipt(receipt);

                    foreach (var detail in RC_Detail)
                    {
                        PRODUCT pro = DataProvider.Ins.DB.PRODUCTs.FirstOrDefault(x => x.PRO_ID == detail.P_ID);
                        pro.PRICE_OUT = detail.PRICE * 1.4m;
                        pro.INSTOCK += detail.QUANTITY;
                    }                    

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBoxCustom m = new MessageBoxCustom("Nhập hàng thành công!", MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                    p.Close();
                }
                else
                {
                    return;
                }
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
