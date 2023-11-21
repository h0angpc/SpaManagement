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

namespace SpaManagement.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

            //Create DataGrid Items Info
            customers.Add(new Customer { Number="1", Character="J", BgColor=(Brush)converter.ConvertFromString("#1098ad"), Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Character="R", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Character="D", BgColor=(Brush)converter.ConvertFromString("#ff8f00"), Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Character="G", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Character="L", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Character="B", BgColor=(Brush)converter.ConvertFromString("#6741d9"), Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Character="S", BgColor=(Brush)converter.ConvertFromString("#ff6d00"), Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Character="A", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Character="F", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Character="S", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Character="J", BgColor=(Brush)converter.ConvertFromString("#1098ad"), Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Character="R", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Character="D", BgColor=(Brush)converter.ConvertFromString("#ff8f00"), Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Character="G", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Character="L", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Character="B", BgColor=(Brush)converter.ConvertFromString("#6741d9"), Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Character="S", BgColor=(Brush)converter.ConvertFromString("#ff6d00"), Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Character="A", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Character="F", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Character="S", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Character="J", BgColor=(Brush)converter.ConvertFromString("#1098ad"), Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Character="R", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Character="D", BgColor=(Brush)converter.ConvertFromString("#ff8f00"), Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Character="G", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Character="L", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Character="B", BgColor=(Brush)converter.ConvertFromString("#6741d9"), Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Character="S", BgColor=(Brush)converter.ConvertFromString("#ff6d00"), Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Character="A", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Character="F", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Character="S", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Character="J", BgColor=(Brush)converter.ConvertFromString("#1098ad"), Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Character="R", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Character="D", BgColor=(Brush)converter.ConvertFromString("#ff8f00"), Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Character="G", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Character="L", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Character="B", BgColor=(Brush)converter.ConvertFromString("#6741d9"), Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Character="S", BgColor=(Brush)converter.ConvertFromString("#ff6d00"), Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Character="A", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Character="F", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Character="S", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customers.Add(new Customer { Number="1", Character="J", BgColor=(Brush)converter.ConvertFromString("#1098ad"), Name="John Doe", Email="john@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="2", Character="R", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Reza Abc", Email="reza@gmail.com", Phone="456-413-879" });
            customers.Add(new Customer { Number="3", Character="D", BgColor=(Brush)converter.ConvertFromString("#ff8f00"), Name="Dennis Bcd", Email="dennis@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="4", Character="G", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Gabriel Sla", Email="gabriel@gmail.com", Phone="001-564-721" });
            customers.Add(new Customer { Number="5", Character="L", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Lena Jkl", Email="lena@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="6", Character="B", BgColor=(Brush)converter.ConvertFromString("#6741d9"), Name="Benjamin Culi", Email="ben@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="7", Character="S", BgColor=(Brush)converter.ConvertFromString("#ff6d00"), Name="Sophia Mur", Email="sophhia@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="8", Character="A", BgColor=(Brush)converter.ConvertFromString("#ff5252"), Name="Ali Ab", Email="ali@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="9", Character="F", BgColor=(Brush)converter.ConvertFromString("#1e88e5"), Name="Frank Word", Email="frank@gmail.com", Phone="159-785-459" });
            customers.Add(new Customer { Number="10", Character="S", BgColor=(Brush)converter.ConvertFromString("#0ca678"), Name="Saeed Sa", Email="sae@gmail.com", Phone="159-785-459" });

            customerDataGrid.ItemsSource = customers;
        }
    }

    public class Customer: INotifyPropertyChanged
    {
        public string character;
        public string Character 
        {
            get { return character; }
            set 
            { 
                character = value;
                OnPropertyChanged(nameof(Character));
            }
        }
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
        public Brush bgcolor;
        public Brush BgColor
        {
            get { return bgcolor; }
            set
            {
                bgcolor = value;
                OnPropertyChanged(nameof(BgColor));
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
