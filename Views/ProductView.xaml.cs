using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        public ProductView()
        {
            InitializeComponent();
            List<Product> List = new List<Product>()
            { new Product(){Name = "Bot rua mat", Price = "250.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"},
              new Product() {Name = "Sua rua mat", Price = "500.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"},
              new Product() {Name = "Sua rua mat", Price = "500.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"},
              new Product() {Name = "Sua rua mat", Price = "500.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"},
              new Product() {Name = "Sua rua mat", Price = "500.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"},
              new Product() {Name = "Sua rua mat", Price = "500.000", imageProduct ="C:\\Users\\quyen\\source\\repos\\SpaManagement\\Images\\spalogo.jpg"}
            };


        }
    }
    public class Product
    {
        public string Name { get; set; }
        public string Price {  get; set; }
        public string imageProduct { get; set; }
    }
}
