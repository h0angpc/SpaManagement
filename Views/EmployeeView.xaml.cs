using SpaManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaManagement.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Page
    {
        public ObservableCollection<Employee> employees { get; set; }
        public EmployeeView()
        {
            InitializeComponent();

            DataContext = this;
            employees = new ObservableCollection<Employee>();


            employees.Add(new Employee { Number="1", Name="Phan Châu Hoàng", Address="157 Cầu Xây, Quận 9", Phone="001-564-721", CCCD="049204001120", Salary=10000000000, Position="Chủ tịch" });
        }

        private void rmvBtn_Click(object sender, RoutedEventArgs e)
        {
            employees.Remove(employeeDataGrid.SelectedItem as Employee);
        }
    }

    public class Employee : INotifyPropertyChanged
    {
        public string number;
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string cccd;

        public string CCCD
        {
            get { return cccd; }
            set
            {
                cccd = value;
                OnPropertyChanged(nameof(CCCD));
            }
        }

        private long salary;

        public long Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        private string position;

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
