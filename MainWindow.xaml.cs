using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SpaManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        bool IsMaximized = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) 
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 780;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }
    }

    public class Customer
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Brush BgColor { get; set; }
    }
}
