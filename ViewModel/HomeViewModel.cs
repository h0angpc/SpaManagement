using LiveCharts;
using SpaManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        private List<string> _datelabels;

        public List<string> datelabels
        {
            get { return _datelabels; }
            set { _datelabels = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _Curvalues;

        public ChartValues<int> Curvalues
        {
            get { return _Curvalues; }
            set { _Curvalues = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _Prevalues;

        public ChartValues<int> Prevalues   
        {
            get { return _Prevalues; }
            set { _Prevalues = value; OnPropertyChanged(); }
        }



        private int _step;
        public int step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
                OnPropertyChanged();
            }
        }

        private int _MaxValueY;

        public int MaxValueY
        {
            get 
            {
                return _MaxValueY; 
            }
            set 
            {
                _MaxValueY = value;
                OnPropertyChanged();
            }
        }


        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadChart {  get; set; }

        public HomeViewModel() 
        {
            MaxValueY = 0;
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EndDate = DateTime.Now;
                StartDate = EndDate.AddDays(-7);
                step = 1;
                Load();
            });

            LoadChart = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EndDate = DateTime.Now;
                StartDate = EndDate.AddDays(-7);
                step = 1;
                Load();
            });
        }

        public void Load()
        {
            datelabels = new List<string>();
            Curvalues = new ChartValues<int>();
            Prevalues = new ChartValues<int>() { 50000, 30000, 60000 , 40000};
            //DateTime datePre = StartDate.AddYears(-1);
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                datelabels.Add(date.ToString("dd/MM/yyyy"));

                int revenue = GetRevenue(date);
                if (revenue > MaxValueY)
                {
                    MaxValueY = revenue;
                }
                Curvalues.Add(revenue);

                //int revenuePre = GetRevenue(datePre);
                //if (revenuePre > MaxValueY)
                //{
                //    MaxValueY = revenuePre;
                //}
                //Prevalues.Add(revenuePre);


            }
            int temp = MaxValueY / 50000;
            MaxValueY = (temp + 1) * 50000;
        }

        public int GetRevenue(DateTime date)
        {
            var querry = DataProvider.Ins.DB.PAYMENTs.Where(hd => hd.DAYTIME.Year == date.Year && hd.DAYTIME.Month == date.Month && hd.DAYTIME.Day == date.Day);
            int totalValues = 0;
            if (querry.Count() > 0)
            {
                foreach (var hoadon in querry)
                {
                    PAYMENT hd = (PAYMENT)hoadon;
                    totalValues += (int)hoadon.PRICE;
                }
                return totalValues;
            }
            return 0;
        }
    }
}
