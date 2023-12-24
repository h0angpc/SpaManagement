using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace SpaManagement.Model
{
    public class ServiceManager
    {
        public static ObservableCollection<SERVICESS> _DatabaseService = new ObservableCollection<SERVICESS>(DataProvider.Ins.DB.SERVICESSes);

        public static ObservableCollection<SERVICESS> GetService()
        {
            return _DatabaseService;
        }

        public static void AddServcie(SERVICESS Service)
        {
            _DatabaseService.Add(Service);
        }
    }
}
