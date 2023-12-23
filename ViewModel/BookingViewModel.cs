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
    }
}
