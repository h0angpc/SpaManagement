using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class BookingViewModel : BaseViewModel
    {
        private ObservableCollection<CUSTOMER> _CustomerList;
        public ObservableCollection<CUSTOMER> CustomerList
        {
            get => _CustomerList;
            set
            {
                _CustomerList = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BOOKING> _BookingList;
        public ObservableCollection<BOOKING> BookingList
        {
            get => _BookingList;
            set
            {
                _BookingList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> filtersource { get; set; }
        private string _TextToFilter;

        public string TextToFilter
        {
            get { return _TextToFilter; }
            set
            {
                _TextToFilter = value;
                OnPropertyChanged(nameof(TextToFilter));
                if (Filtercondition == "Họ tên")
                {
                    BookingCollection.Filter = FilterByName;
                }
                else if (Filtercondition == "Ngày")
                {
                    BookingCollection.Filter = FilterByDate;
                }
                else if (Filtercondition == "Mã BK")
                {
                    BookingCollection.Filter = FilterByBKMa;
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

        public ICommand ShowAddBookCommand { get; set; }
        public ICommand ShowEditBookCommand { get; set; }
        private ICollectionView _bookingCollection;
        public ICollectionView BookingCollection
        {
            get { return _bookingCollection; }
            set
            {
                _bookingCollection = value;
            }
        }

        public BookingViewModel()
        {
            filtersource = new ObservableCollection<string> { "Họ tên", "Mã BK", "Ngày" };
            Filtercondition = "Họ tên"; // Default value
            _CustomerList = CustomerManager.GetCustomers();
            _BookingList = BookingManager.GetBOOKINGs();
            BookingCollection = CollectionViewSource.GetDefaultView(_BookingList);

            ShowAddBookCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddBookingView wd = new AddBookingView();
                AddBookingViewModel vm = new AddBookingViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
            });
            ShowEditBookCommand = new RelayCommand<BOOKING>((p) => { return p != null; }, (p) => {
                if (p != null)
                {
                    var _theBooking = DataProvider.Ins.DB.BOOKINGs.FirstOrDefault(x => x.BK_ID == p.BK_ID);
                    _theBooking.IS_EDITED = true;
                    DataProvider.Ins.DB.SaveChanges();
                    EditBookingViewModel editViewModel = new EditBookingViewModel(p);
                    editViewModel.EditCompleted += EditViewModel_EditCompleted;
                    EditBookingView editView = new EditBookingView();
                    editView.DataContext = editViewModel;
                    editView.ShowDialog();
                }
            });
            //DeleteExpiredBooking(_BookingList, BookingCollection);
        }
        private bool FilterByName(object book)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var bookDetail = book as BOOKING;
                if (bookDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string customerName = bookDetail.CUSTOMER.CUS_NAME.ToLower();

                    return customerName.Contains(filtertext);
                }
            }
            return true;
        }
        private bool FilterByBKMa(object MA)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var bookDetail = MA as BOOKING;
                if (bookDetail!= null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string bookingMA = bookDetail.BK_MA.ToLower();

                    return bookingMA.Contains(filtertext);
                }
            }
            return true;
        }

        private bool FilterByDate(object book)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var bookDetail = book as BOOKING;
                if (bookDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string date = bookDetail.START_TIME.ToString("dd/MM/yyyy HH:mm:ss");

                    return date.Contains(filtertext);
                }
            }
            return true;
        }
        private void EditViewModel_EditCompleted(object sender, EventArgs e)
        {
            _BookingList = BookingManager.GetBOOKINGs();
            BookingCollection = CollectionViewSource.GetDefaultView(_BookingList);
            BookingCollection.Refresh();
        }

        public  void DeleteExpiredBooking(ObservableCollection<BOOKING> _BookingList, ICollectionView BookingCollection)
        {
            var expiredBookings = _BookingList.Where(booking => DateTime.Now.Subtract(booking.START_TIME).Days >= 1).ToList();

            foreach (var booking in expiredBookings)
            {
                DataProvider.Ins.DB.BOOKINGs.Remove(booking);
                _BookingList.Remove(booking);
            }

            DataProvider.Ins.DB.SaveChanges();
            BookingCollection.Refresh();
        }
    }
}
