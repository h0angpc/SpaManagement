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
using System.Windows.Data;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class ReceiptViewModel:BaseViewModel
    {
        public ObservableCollection<string> filtersource { get; set; }
        private string _TextToFilter;
        public string TextToFilter
        {
            get { return _TextToFilter; }
            set
            {
                _TextToFilter = value;
                OnPropertyChanged(nameof(TextToFilter));

                if (Filtercondition == "Ngày")
                {
                    ReceiptCollection.Filter = FilterByDate;
                }
                else if (Filtercondition == "Mã NH")
                {
                    ReceiptCollection.Filter = FilterByMA;
                }

            }
        }

        private string _filtercondition;
        public string Filtercondition
        {
            get
            {
                return _filtercondition;
            }
            set
            {
                _filtercondition = value;
                OnPropertyChanged(nameof(Filtercondition));
            }
        }

        private ObservableCollection<RECEIPT> _ReceiptList;
        public ObservableCollection<RECEIPT> ReceiptList
        {
            get => _ReceiptList;
            set
            {
                _ReceiptList = value;
                OnPropertyChanged();
            }
        }
        public ICommand ShowAddRecCommand { get; set; }
        public ICommand ShowCTNHCommand { get; set; }

        private ICollectionView _receiptCollection;
        public ICollectionView ReceiptCollection
        {
            get { return _receiptCollection; }
            set
            {
                _receiptCollection = value;
            }
        }

        public ReceiptViewModel()
        {
            filtersource = new ObservableCollection<string> { "Mã NH", "Ngày" };
            Filtercondition = "Ngày"; // Default value

            _ReceiptList = ReceiptManager.GetReceipt();

            ReceiptCollection = CollectionViewSource.GetDefaultView(_ReceiptList);
            ShowAddRecCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RECEIPT rec = new RECEIPT { REC_DATE = DateTime.Now, REC_PRICE = 0 };

                DataProvider.Ins.DB.RECEIPTs.Add(rec);
                DataProvider.Ins.DB.SaveChanges();

                AddReceiptViewModel vm = new AddReceiptViewModel(rec);
                AddReceiptView wd = new AddReceiptView
                {
                    DataContext = vm
                };
                wd.ShowDialog();
            });

            ShowCTNHCommand = new RelayCommand<RECEIPT>((p) => { return p != null; }, (p) =>
            {
                if (p != null)
                {
                    CTNHViewModel vm = new CTNHViewModel(p);
                    CTNHView CTView = new CTNHView
                    {
                        DataContext = vm
                    };
                    CTView.ShowDialog();
                }

            });
        }

        private bool FilterByMA(object rec)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var recDetail = rec as RECEIPT;
                if (recDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string reciptMA = recDetail.REC_MA.ToLower();

                    return reciptMA.Contains(filtertext);
                }
            }
            return true;
        }

        private bool FilterByDate(object rec)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var recDetail = rec as RECEIPT;
                //recDetail.REC_DATE = DateTime.Now;
                if (recDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string date = recDetail.REC_DATE.ToString("dd/MM/yyyy HH:mm:ss");

                    return date.Contains(filtertext);
                }
            }
            return true;
        }
    }
}
