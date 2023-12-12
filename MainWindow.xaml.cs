
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace SpaManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }  

        bool IsMaximized = false;
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void controlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void controlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void controlBar_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void maxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
                this.WindowState = WindowState.Normal;
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    mContainer.Navigate(new System.Uri("Pages/HomePage.xaml", System.UriKind.RelativeOrAbsolute));
        //    homeBtn.IsChecked = true;
        //}

        //private void cusBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    mContainer.Navigate(new System.Uri("Pages/CustomerPage.xaml", System.UriKind.RelativeOrAbsolute));
        //}


        //private void homeBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    mContainer.Navigate(new System.Uri("Pages/HomePage.xaml", System.UriKind.RelativeOrAbsolute));
        //}

        //private void empBtn_Checked(object sender, RoutedEventArgs e)
        //{
        //    mContainer.Navigate(new System.Uri("Pages/EmployeePage.xaml", System.UriKind.RelativeOrAbsolute));
        //}
    }

    
}
