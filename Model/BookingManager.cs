using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Model
{
    internal class BookingManager
    {
        public static ObservableCollection<BOOKING> _DatabaseBookings = new ObservableCollection<BOOKING>(DataProvider.Ins.DB.BOOKINGs);
        public static ObservableCollection<BOOKING> GetBOOKINGs() 
        { 
            return _DatabaseBookings; 
        }
        public static ObservableCollection<CUSTOMER> _DatabaseCustomers = new ObservableCollection<CUSTOMER>(DataProvider.Ins.DB.CUSTOMERs);

        public static ObservableCollection<CUSTOMER> GetCustomers()
        {
            return _DatabaseCustomers;
        }
        public static void AddBooking(BOOKING booking)
        {
            _DatabaseBookings.Add(booking);

        }
    }
}
