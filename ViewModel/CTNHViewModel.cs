using SpaManagement.Model;
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
        }

        void LoadCTNH(RECEIPT SelectedReceipt)
        {
            CTNHsource = new ObservableCollection<RECEIPT_DETAIL>(DataProvider.Ins.DB.RECEIPT_DETAIL.Where(x => x.REC_ID == SelectedReceipt.REC_ID));
        }
    }
}
