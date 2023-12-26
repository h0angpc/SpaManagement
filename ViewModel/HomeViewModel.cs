using LiveCharts;
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

        private ChartValues<int> _values;

        public ChartValues<int> values
        {
            get { return _values; }
            set { _values = value; OnPropertyChanged(); }
        }



        private int _step;
        public int step
        {
            get;
            set;
        }

        public ICommand LoadedWindowCommand { get; set; }

        public HomeViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EndDate = DateTime.Now;
                StartDate = EndDate.AddDays(-30);
                Load();
            });
        }

        void Load()
        {
            datelabels = new List<string>();
            values = new ChartValues<int>();
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                datelabels.Add(date.ToString("dd/MM/yyyy"));
                values.Add(100);
            }
            step = 5;
        }
    }
}
