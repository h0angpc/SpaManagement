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
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        public ObservableCollection<Customer> customers { get; set; }
        public CustomerView()
        {
            InitializeComponent();

            DataContext = this;

            customers = new ObservableCollection<Customer>();



            //Create DataGrid Items Info
            customers.Add(new Customer { Number="1", Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            //customerDataGrid.ItemsSource = customers;
        }

        private void rmvBtn_Click(object sender, RoutedEventArgs e)
        {
            customers.Remove(customerDataGrid.SelectedItem as Customer);
            //foreach (var i in customerDataGrid.SelectedItems) 
            //{
            //    customers.Remove(i as Customer);
            //}
        }
        private void addCus_btn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerView addCustomer = new AddCustomerView();
            addCustomer.ShowDialog();
        }

        private void customerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null)
                return;

            var row = FindVisualParent<DataGridRow>(e.OriginalSource as DependencyObject);
            if (row == null)
                return;

            Customer rowData = (Customer)row.Item;

            MessageBox.Show(rowData.Name);
        }

        private static T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T parent)
                    return parent;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }

    public class Customer : INotifyPropertyChanged
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
        public string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
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
