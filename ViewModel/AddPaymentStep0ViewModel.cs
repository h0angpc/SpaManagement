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
using System.Xml.Linq;

namespace SpaManagement.ViewModel
{
    public class AddPaymentStep0ViewModel: BaseViewModel
    {
        private ObservableCollection<string> _CusSource;
        public ObservableCollection<string> CusSource
        {
            get => _CusSource;
            set
            {
                _CusSource = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedCus;

        public string SelectedCus
        {
            get 
            {
                return _SelectedCus; 
            }
            set 
            {
                _SelectedCus = value;
                OnPropertyChanged();
            }
        }


        public ICommand ShowCreatePayment { get; set; }
        public ICommand CloseCommand { get; set; }

        public AddPaymentStep0ViewModel() 
        {
            CusSource = new ObservableCollection<string>(DataProvider.Ins.DB.CUSTOMERs.Select(x => x.CUS_MA + " | " + x.CUS_NAME).ToList());

            ShowCreatePayment = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string cusMA = SelectedCus.Split('|')[0].Trim();

                int cusID = DataProvider.Ins.DB.CUSTOMERs.FirstOrDefault(x => x.CUS_MA == cusMA).CUS_ID;

                PAYMENT payment = new PAYMENT() { C_ID = cusID, DAYTIME = DateTime.Now, PRICE = 0 };

                DataProvider.Ins.DB.PAYMENTs.Add(payment);
                DataProvider.Ins.DB.SaveChanges();

                //PaymentManager.AddPayment(payment);

                AddPaymentView wd = new AddPaymentView();
                AddPaymentViewModel vm = new AddPaymentViewModel(payment, SelectedCus);

                wd.DataContext = vm;
                p.Close();
                wd.ShowDialog();
            });

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });
        }
    }
}
