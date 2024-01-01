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
    }
}
