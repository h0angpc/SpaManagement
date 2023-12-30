using LiveCharts;
using SpaManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;

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

        private DataTable topProduct;
        public DataTable TopProduct
        {
            get => topProduct;
            set
            {
                topProduct = value;
                OnPropertyChanged();
            }
        }

        private DataTable _TopService;
        public DataTable TopService
        {
            get => _TopService;
            set
            {
                _TopService = value;
                OnPropertyChanged();
            }
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

        private int _TotalRevenue;

        public int TotalRevenue
        {
            get { return _TotalRevenue; }
            set { _TotalRevenue = value; OnPropertyChanged(); }
        }

        private int _PreTotalRevenue;

        public int PreTotalRevenue
        {
            get { return _PreTotalRevenue; }
            set { _PreTotalRevenue = value; OnPropertyChanged(); }
        }

        private string _PercentRevenue;

        public string PercentRevenue
        {
            get { return _PercentRevenue; }
            set { _PercentRevenue = value; OnPropertyChanged(); }
        }

        private Brush _PercentColor;

        public Brush PercentColor
        {
            get { return _PercentColor; }
            set { _PercentColor = value; OnPropertyChanged(); }
        }

        private string _TodayRevenue;

        public string TodayRevenue
        {
            get 
            {
                return _TodayRevenue; 
            }
            set
            {
                _TodayRevenue = value;
                OnPropertyChanged();
            }
        }

        private string _TotalProductThisMonth;

        public string TotalProductThisMonth
        {
            get
            {
                return _TotalProductThisMonth;
            }
            set
            {
                _TotalProductThisMonth = value;
                OnPropertyChanged();
            }
        }

        private string _Visibility;

        public string Visibility
        {
            get { return _Visibility; }
            set { _Visibility = value; OnPropertyChanged(); }
        }


        public ICommand LoadedPageCommand { get; set; }
        public ICommand LoadChart {  get; set; }
        public ICommand ShowPreChart {  get; set; }

        public HomeViewModel() 
        {
            step = 1;
            TodayRevenue = "";
            MaxValueY = 0;
            TotalRevenue = 0;
            PreTotalRevenue = 0;
            PercentRevenue = "";
            Visibility = "Hidden";

            LoadedPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadInfoCard();
                LoadTopProduct(4);
                LoadTopService(4);

                EndDate = DateTime.Now;
                StartDate = EndDate.AddDays(-7);
                Load();
            });

            LoadChart = new RelayCommand<object>((p) =>
            {
                if (EndDate <= StartDate)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                TotalRevenue = 0;
                PreTotalRevenue = 0;
                MaxValueY = 0;
                PercentRevenue = "";
                Load();
            });

            ShowPreChart = new RelayCommand<CheckBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.IsChecked == true)
                {
                    Visibility = "Visible";
                }
                else
                {
                    Visibility = "Hidden";
                }
            });
        }

        public void Load()
        {
            datelabels = new List<string>();
            Curvalues = new ChartValues<int>();
            Prevalues = new ChartValues<int>();
            DateTime datePre = StartDate.AddYears(-1);
            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                datelabels.Add(date.ToString("dd/MM/yyyy"));

                int revenue = GetRevenue(date);
                TotalRevenue += revenue;
                if (revenue > MaxValueY)
                {
                    MaxValueY = revenue;
                }
                Curvalues.Add(revenue);

                int revenuePre = GetRevenue(datePre);
                PreTotalRevenue += revenuePre;
                if (revenuePre > MaxValueY)
                {
                    MaxValueY = revenuePre;
                }
                Prevalues.Add(revenuePre);

                datePre = datePre.AddDays(1);

            }

            if (Curvalues.Count() > 9) 
            {
                step = Curvalues.Count() / 5;
            }
            else
            {
                step = 1;
            }

            int temp = MaxValueY / 50000;
            MaxValueY = (temp + 1) * 50000;

            if (TotalRevenue >= PreTotalRevenue)
            {
                float percent = (float)(TotalRevenue - PreTotalRevenue) / (float)(PreTotalRevenue);
                PercentRevenue = percent.ToString("P2").Replace(".", ",");
                Color color = (Color)ColorConverter.ConvertFromString("#11D13B");
                SolidColorBrush brush = new SolidColorBrush(color);
                PercentColor = brush;
            }
            else
            {
                float percent = (float)(PreTotalRevenue - TotalRevenue) / (float)(PreTotalRevenue);
                PercentRevenue = percent.ToString("P2").Replace(".", ",");
                Color color = (Color)ColorConverter.ConvertFromString("Red");
                SolidColorBrush brush = new SolidColorBrush(color);
                PercentColor = brush;
            }
        }

        public void LoadInfoCard()
        {
            int tdayrevenue = GetRevenue(DateTime.Now);

            TodayRevenue = string.Format("{0:N0} VND", tdayrevenue);

            LoadMonthTotalProduct(DateTime.Now.Month);

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

        public void LoadTopProduct(int top)
        {

            QUANLYSPAEntities db = new QUANLYSPAEntities();

            string conStr = db.Database.Connection.ConnectionString;

            SqlConnection connection = new SqlConnection(conStr);

            connection.Open();

            string query = String.Format("EXECUTE TOP_SALES @TOP = {0}", top);

            SqlCommand command = new SqlCommand(query, connection);

            DataTable UsersTalbe = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(UsersTalbe);

            connection.Close();

            TopProduct = UsersTalbe;
        }

        public void LoadTopService(int top)
        {

            QUANLYSPAEntities db = new QUANLYSPAEntities();

            string conStr = db.Database.Connection.ConnectionString;

            SqlConnection connection = new SqlConnection(conStr);

            connection.Open();

            string query = String.Format("EXECUTE TOP_SERVICE @TOP = {0}", top);

            SqlCommand command = new SqlCommand(query, connection);

            DataTable UsersTalbe = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(UsersTalbe);

            connection.Close();

            TopService = UsersTalbe;
        }

        public void LoadMonthTotalProduct(int month)
        {
            var a = DataProvider.Ins.DB.PAYMENT_DETAIL_PRODUCT.Where(x => x.PAYMENT.DAYTIME.Month == month);
            if (a.Count() < 0)
            {
                TotalProductThisMonth = "0";
            }
            else
            {
                QUANLYSPAEntities db = new QUANLYSPAEntities();

                string conStr = db.Database.Connection.ConnectionString;

                SqlConnection connection = new SqlConnection(conStr);

                connection.Open();

                string query = String.Format("EXEC dbo.TotalProduct @m = {0}", month);

                SqlCommand command = new SqlCommand(query, connection);

                DataTable TotalProduct = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(TotalProduct);

                connection.Close();

                try
                {
                    TotalProductThisMonth = Convert.ToString(TotalProduct.Rows[0][0]);
                }
                catch
                {

                }
            }
        }
    }
}
