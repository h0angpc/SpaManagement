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
    public class EmployeeViewModel:BaseViewModel
    {
        public ObservableCollection<string> filtersource { get; set; }
        private ObservableCollection<EMPLOYEE> _EmployeeList;
        public ObservableCollection<EMPLOYEE> EmployeeList
        {
            get => _EmployeeList;
            set
            {
                _EmployeeList = value;
                OnPropertyChanged();
            }
        }
        public ICommand ShowAddEmpCommand { get; set; }
        public ICommand ShowEditEmpCommand { get; set; }

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
                    EmployeeCollection.Filter = FilterByName;
                }
                else if (Filtercondition == "Mã NV")
                {
                    EmployeeCollection.Filter = FilterByID;
                }
            }
        }

        private ICollectionView _employeeCollection;

        public ICollectionView EmployeeCollection
        {
            get { return _employeeCollection; }
            set
            {
                _employeeCollection = value;
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

        public EmployeeViewModel()
        {
            filtersource = new ObservableCollection<string> { "Họ tên", "Mã NV" };
            Filtercondition = "Họ tên"; // Default value

            _EmployeeList = EmployeeManager.GetEmployees();
            EmployeeCollection = CollectionViewSource.GetDefaultView(_EmployeeList);

            ShowAddEmpCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddEmployeeView wd = new AddEmployeeView(); wd.ShowDialog(); });
        }

        private bool FilterByName(object emp)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var empDetail = emp as EMPLOYEE;
                if (empDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string empName = empDetail.EMP_DISPLAYNAME.ToLower();

                    return empName.Contains(filtertext);
                }
            }
            return true;
        }

        private bool FilterByID(object emp)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var cusDetail = emp as EMPLOYEE;
                if (cusDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string empMA = cusDetail.EMP_MA.ToLower();

                    return empMA.Contains(filtertext);
                }
            }
            return true;
        }
    }
}
