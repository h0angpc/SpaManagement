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
    public class AddBookingViewModel : BaseViewModel
    {
        private ObservableCollection<string> _CusSource;
        public ObservableCollection<string> CusSource { get => _CusSource; set { _CusSource = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _EmpSource;
        public ObservableCollection<string> EmpSource { get => _EmpSource; set { _EmpSource = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _SerSource;
        public ObservableCollection<string> SerSource { get => _SerSource; set { _SerSource = value; OnPropertyChanged(); } }

        private ObservableCollection<TimeSpan> _startTime;
        public ObservableCollection<TimeSpan> StartTime { get => _startTime; set { _startTime = value; OnPropertyChanged(); } }
        
        private ObservableCollection<TimeSpan> _endTime;
        public ObservableCollection<TimeSpan> EndTime { get => _endTime; set { _endTime = value; OnPropertyChanged(); } }

        private DateTime _selectedDate;
        public DateTime SelectedDate { get { return _selectedDate; } set { _selectedDate = value; OnPropertyChanged(); } }

        private TimeSpan _selectedStart;
        public TimeSpan SelectedStart { get { return _selectedStart; } set { _selectedStart = value; OnPropertyChanged(nameof(SelectedStart)); UpdateEndTime(); } }

        private TimeSpan _selectedEnd;
        public TimeSpan SelectedEnd { get { return _selectedEnd; } set { _selectedEnd = value; OnPropertyChanged(); } }

        private string _selectedCus;
        public string SelectedCus { get { return _selectedCus; } set { _selectedCus = value; OnPropertyChanged(); } }

        private string _selectedEmp;
        public string SelectedEmp { get { return _selectedEmp; } set { _selectedEmp = value; OnPropertyChanged(); } }

        private string _selectedSer;
        public string SelectedSer { get { return _selectedSer; } set { _selectedSer = value; OnPropertyChanged(); } }

        int SerID;
        string CusMA;
        int CusID;
        string EmpMA;
        int EmpID;

        DateTime DB_startTime = new DateTime(); // 
        DateTime DB_endTime = new DateTime(); //
        // dùng timespan biểu diễn giờ 
        // để gáng vô database thì sẽ kết hợp với Date để đưa vào

        public ICommand AddBookingCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public AddBookingViewModel() 
        {
            SelectedDate = DateTime.Today;
            CusSource = new ObservableCollection<string>(DataProvider.Ins.DB.CUSTOMERs.Select(x => x.CUS_MA + " | " + x.CUS_NAME).ToList());
            SerSource = new ObservableCollection<string>(DataProvider.Ins.DB.SERVICESSes.Where(x => x.IS_DELETED == false).Select(x => x.SER_NAME).ToList());

            StartTime = new ObservableCollection<TimeSpan>();
            EndTime = new ObservableCollection<TimeSpan>();
            for (int hour = 8; hour < 20; hour++)
            {
                TimeSpan time = new TimeSpan(hour, 0, 0);
                StartTime.Add(time);
            }
            
            // Retrieve all employee IDs who have bookings within the selected time frame
            //var bookedEmployees = DataProvider.Ins.DB.BOOKINGs
            //    .Where(booking => booking.END_TIME > DB_startTime && booking.START_TIME < DB_endTime)
            //    .Select(booking => booking.E_ID.ToString())
            //    .ToList();

            ////Retrieve employees who are not in the bookedEmployees list and match the role "Dịch vụ"
            //EmpSource = new ObservableCollection<string>(
            //    DataProvider.Ins.DB.EMPLOYEEs
            //        .Where(employee => employee.EMP_ROLE == "Dịch vụ" && !bookedEmployees.Contains(employee.EMP_ID.ToString()))
            //        .Select(employee => employee.EMP_MA + " | " + employee.EMP_DISPLAYNAME)
            //        .ToList()
            //);


            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedStart) || e.PropertyName == nameof(SelectedEnd) || e.PropertyName == nameof(SelectedDate))
                {
                    // Check if both SelectedStart and SelectedEnd have valid values
                    if (SelectedStart != default(TimeSpan) && SelectedEnd != default(TimeSpan))
                    {
                        // Update EmpSource based on the selected start and end times
                        DB_startTime = SelectedDate.Add(SelectedStart);
                        DB_endTime = SelectedDate.Add(SelectedEnd);

                        var bookedEmployees = DataProvider.Ins.DB.BOOKINGs
                            .Where(booking => booking.END_TIME > DB_startTime && booking.START_TIME < DB_endTime)
                            .Select(booking => booking.E_ID.ToString())
                            .ToList();

                        EmpSource = new ObservableCollection<string>(
                            DataProvider.Ins.DB.EMPLOYEEs
                                .Where(employee => employee.EMP_ROLE == "Dịch vụ" && !bookedEmployees.Contains(employee.EMP_ID.ToString()))
                                .Select(employee => employee.EMP_MA + " | " + employee.EMP_DISPLAYNAME)
                                .ToList()
                        );
                    }
                    else
                    {
                        // If SelectedStart or SelectedEnd is not selected, clear EmpSource
                        EmpSource = new ObservableCollection<string>();
                    }
                }
            };



            AddBookingCommand = new RelayCommand<object>((p) =>
            {
                return !string.IsNullOrEmpty(SelectedCus)
                   && !string.IsNullOrEmpty(SelectedEmp)
                   && !string.IsNullOrEmpty(SelectedSer)
                   && SelectedDate != default(DateTime)
                   && SelectedStart != default(TimeSpan)
                   && SelectedEnd != default(TimeSpan);

            }, (p) =>
            {

                CusMA = SelectedCus.Split('|')[0].Trim();
                CusID = DataProvider.Ins.DB.CUSTOMERs.FirstOrDefault(x => x.CUS_MA == CusMA).CUS_ID;
                SerID = DataProvider.Ins.DB.SERVICESSes.FirstOrDefault(x => x.SER_NAME == SelectedSer).SER_ID;
                DB_startTime = SelectedDate.Add(SelectedStart);
                DB_endTime = SelectedDate.Add(SelectedEnd);
                EmpMA = SelectedEmp.Split('|')[0].Trim();
                EmpID = DataProvider.Ins.DB.EMPLOYEEs.FirstOrDefault(x => x.EMP_MA == EmpMA).EMP_ID;
                var newBooking = new BOOKING()
                {
                    C_ID = CusID,
                    E_ID = EmpID,
                    S_ID = SerID,
                    START_TIME = DB_startTime,
                    END_TIME = DB_endTime,
                    IS_EDITED = false
                };
                DataProvider.Ins.DB.BOOKINGs.Add(newBooking);
                DataProvider.Ins.DB.SaveChanges(); 
                BookingManager.AddBooking(newBooking);
                SelectedCus = null;
                SelectedEmp = null;
                SelectedSer = null;
                SelectedDate = DateTime.Today;
                MessageBoxCustom m = new MessageBoxCustom("Thêm booking mới thành công", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();


            });




            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });
        }
        private void UpdateEndTime()
        {
            EndTime.Clear(); // Clear existing items in EndTime collection

            if (SelectedStart != default(TimeSpan) && SelectedStart.Hours >= 8 && SelectedStart.Hours < 20)
            {
                int selectedHour = SelectedStart.Hours;
                SelectedEnd = TimeSpan.Zero;

                // Start populating EndTime from the hour after SelectedStart
                for (int i = selectedHour + 1; i <= 20; i++)
                {
                    TimeSpan time = new TimeSpan(i, 0, 0);
                    EndTime.Add(time);
                }
            }
        }
    }
}
