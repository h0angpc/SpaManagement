﻿using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Security.Cryptography.X509Certificates;

namespace SpaManagement.ViewModel
{
    public class EditBookingViewModel : BaseViewModel
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
        public TimeSpan SelectedStart { get { return _selectedStart; } set { _selectedStart = value; OnPropertyChanged(nameof(SelectedStart)); UpdateEndTime(); SelectedEnd = TimeSpan.Zero; } }

        private TimeSpan _selectedEnd;
        public TimeSpan SelectedEnd { get { return _selectedEnd; } set { _selectedEnd = value; OnPropertyChanged(nameof(SelectedEnd));   } }

        private string _selectedCus;
        public string SelectedCus { get { return _selectedCus; } set { _selectedCus = value; OnPropertyChanged(); } }

        private string _selectedEmp;
        public string SelectedEmp { get { return _selectedEmp; } set { _selectedEmp = value; OnPropertyChanged(); } }

        private string _selectedSer;
        public string SelectedSer { get { return _selectedSer; } set { _selectedSer = value; OnPropertyChanged(); } }

        public EventHandler EditCompleted;

        int SerID;
        string CusMA;
        int CusID;
        string EmpMA;
        int EmpID;
 
        DateTime DB_startTime = new DateTime(); // 
        DateTime DB_endTime = new DateTime(); //
        // dùng timespan biểu diễn giờ 
        // để gáng vô database thì sẽ kết hợp với Date để đưa vào

        public ICommand EditBookingCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public EditBookingViewModel(BOOKING SelectedBooking)
        {
            // Hiển thị thông tin lần đầu trước khi sửa 
           
            DataProvider.Ins.DB.SaveChanges();
            CusSource = new ObservableCollection<string>(DataProvider.Ins.DB.CUSTOMERs.Select(x => x.CUS_MA + " | " + x.CUS_NAME).ToList());
            SerSource = new ObservableCollection<string>(DataProvider.Ins.DB.SERVICESSes.Where(x => x.IS_DELETED == false).Select(x => x.SER_NAME).ToList());

            StartTime = new ObservableCollection<TimeSpan>();
            EndTime = new ObservableCollection<TimeSpan>();
            for (int hour = 8; hour <= 20; hour++)
            {
                TimeSpan time = new TimeSpan(hour, 0, 0);
                StartTime.Add(time);

            }
            SelectedDate = SelectedBooking.END_TIME.Date;
            SelectedCus = DataProvider.Ins.DB.CUSTOMERs.Where(x => x.CUS_ID == SelectedBooking.C_ID).Select(x => x.CUS_MA + " | " + x.CUS_NAME).SingleOrDefault().ToString();
            SelectedSer = DataProvider.Ins.DB.SERVICESSes.Where(x => x.SER_ID == SelectedBooking.S_ID).Select(x => x.SER_NAME).SingleOrDefault().ToString();
            SelectedStart = SelectedBooking.START_TIME.TimeOfDay;
            SelectedEnd = SelectedBooking.END_TIME.TimeOfDay;
            DB_startTime = SelectedDate.Add(SelectedStart);
            DB_endTime = SelectedDate.Add(SelectedEnd);
            SelectedEmp = DataProvider.Ins.DB.EMPLOYEEs.Where(x => x.EMP_ID == SelectedBooking.E_ID).Select(x => x.EMP_MA + " | " + x.EMP_DISPLAYNAME).SingleOrDefault().ToString();
            var bookedEmployees = DataProvider.Ins.DB.BOOKINGs
                .Where(booking => (booking.END_TIME > DB_startTime && booking.START_TIME < DB_endTime)
                )
                .Select(booking => booking.E_ID.ToString())
                .ToList();
            int emp_Editing = DataProvider.Ins.DB.BOOKINGs
                .Where(booking => booking.IS_EDITED == true)
                .Select(booking => booking.E_ID)
                .SingleOrDefault();
            EmpSource = new ObservableCollection<string>(
                DataProvider.Ins.DB.EMPLOYEEs
                    .Where(employee => (employee.EMP_ROLE == "Dịch vụ" && !bookedEmployees.Contains(employee.EMP_ID.ToString())) || employee.EMP_ID == emp_Editing)
                    .Select(employee => employee.EMP_MA + " | " + employee.EMP_DISPLAYNAME)
                    .ToList()

            );

            // End cho phần show thông tin lần đầu

            

            EditBookingCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(SelectedCus)
                   || string.IsNullOrEmpty(SelectedEmp)
                   || string.IsNullOrEmpty(SelectedSer)
                   || SelectedDate == default(DateTime)
                   || SelectedStart == default(TimeSpan)
                   || SelectedEnd == default(TimeSpan))
                {
                    return false;
                }
                var _serID = DataProvider.Ins.DB.SERVICESSes.FirstOrDefault(x => x.SER_NAME == SelectedSer).SER_ID;
                var _empMA = SelectedEmp.Split('|')[0].Trim();
                var _empID = DataProvider.Ins.DB.EMPLOYEEs.FirstOrDefault(x => x.EMP_MA == _empMA).EMP_ID;
                var _startTime = SelectedDate + SelectedStart;
                var _endTime = SelectedDate + SelectedEnd;


                var displaylist = DataProvider.Ins.DB.BOOKINGs.Where(x => x.S_ID == _serID && x.E_ID == _empID && x.START_TIME == _startTime && x.END_TIME == _endTime);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                CusMA = SelectedCus.Split('|')[0].Trim();
                CusID = DataProvider.Ins.DB.CUSTOMERs.FirstOrDefault(x => x.CUS_MA == CusMA).CUS_ID;
                SerID = DataProvider.Ins.DB.SERVICESSes.FirstOrDefault(x => x.SER_NAME == SelectedSer).SER_ID;
                DB_startTime = SelectedDate.Add(SelectedStart);
                DB_endTime = SelectedDate.Add(SelectedEnd);
                EmpMA = SelectedEmp.Split('|')[0].Trim();
                EmpID = DataProvider.Ins.DB.EMPLOYEEs.FirstOrDefault(x => x.EMP_MA == EmpMA).EMP_ID;
                var _selectedBooking = DataProvider.Ins.DB.BOOKINGs.FirstOrDefault(x => x.BK_ID == SelectedBooking.BK_ID);
                if (_selectedBooking != null)
                {
                    _selectedBooking.S_ID = SerID;
                    _selectedBooking.E_ID = EmpID;
                    _selectedBooking.C_ID = CusID;
                    _selectedBooking.START_TIME = DB_startTime;
                    _selectedBooking.END_TIME = DB_endTime;
                    DataProvider.Ins.DB.SaveChanges();
                    OnEditCompleted(EventArgs.Empty);
                }
                MessageBoxCustom m = new MessageBoxCustom("Cập nhật thành công!", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            })
            {
            };
            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                var _selectedBooking = DataProvider.Ins.DB.BOOKINGs.FirstOrDefault(x => x.BK_ID == SelectedBooking.BK_ID);
                _selectedBooking.IS_EDITED = false;
                DataProvider.Ins.DB.SaveChanges();
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });


            // Load lại EMP_Source với mỗi lần thay đổi thời gian booking
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SelectedStart) || e.PropertyName == nameof(SelectedEnd) || e.PropertyName == nameof(SelectedDate))
                {
                    // Check if both SelectedStart and SelectedEnd have valid values
                    if (SelectedStart != default(TimeSpan) && SelectedEnd != default(TimeSpan))
                    {
                        DB_startTime = SelectedDate.Add(SelectedStart);
                        DB_endTime = SelectedDate.Add(SelectedEnd);
                        // nếu giờ chọn lại collasped với giờ ban đầu của SelectedBooking thì sẽ có SelectedEmp trong đó
                        if (SelectedBooking.END_TIME > DB_startTime && SelectedBooking.START_TIME < DB_endTime) //selectedb_end > selectedStart && selectedb_start < selectedend
                        {
                            var _bookedEmployees = DataProvider.Ins.DB.BOOKINGs
                            .Where(booking => (booking.END_TIME > DB_startTime && booking.START_TIME < DB_endTime)
                            )
                            .Select(booking => booking.E_ID.ToString())
                            .ToList();
                            int _emp_Editing = DataProvider.Ins.DB.BOOKINGs
                                .Where(booking => booking.IS_EDITED == true)
                                .Select(booking => booking.E_ID)
                                .SingleOrDefault();
                            EmpSource = new ObservableCollection<string>(
                                DataProvider.Ins.DB.EMPLOYEEs
                                    .Where(employee => (employee.EMP_ROLE == "Dịch vụ" && !_bookedEmployees.Contains(employee.EMP_ID.ToString())) || employee.EMP_ID == _emp_Editing)
                                    .Select(employee => employee.EMP_MA + " | " + employee.EMP_DISPLAYNAME)
                                    .ToList()
                            );
                        }
                        // Nếu không thì sẽ loai EmpSource như bình thường
                        else
                        {
                            var _bookedEmployees = DataProvider.Ins.DB.BOOKINGs
                        .Where(booking => (booking.END_TIME > DB_startTime && booking.START_TIME < DB_endTime)
                        )
                        .Select(booking => booking.E_ID.ToString())
                        .ToList();
                            EmpSource = new ObservableCollection<string>(
                                DataProvider.Ins.DB.EMPLOYEEs
                                    .Where(employee => (employee.EMP_ROLE == "Dịch vụ" && !_bookedEmployees.Contains(employee.EMP_ID.ToString())))
                                    .Select(employee => employee.EMP_MA + " | " + employee.EMP_DISPLAYNAME)
                                    .ToList()
                            );
                        }
                    }
                   
                }
            };
        }
        private void UpdateEndTime()
        {
            EndTime.Clear(); // Clear existing items in EndTime collection

            if (SelectedStart != null && SelectedStart.Hours >= 8 && SelectedStart.Hours < 20)
            {
                int selectedHour = SelectedStart.Hours;

                // Start populating EndTime from the hour after SelectedStart
                for (int i = selectedHour + 1; i <= 20; i++)
                {
                    TimeSpan time = new TimeSpan(i, 0, 0);
                    EndTime.Add(time);
                }
            }
        }
        protected virtual void OnEditCompleted(EventArgs e)
        {
            EditCompleted?.Invoke(this, e);
        }

    }
}
