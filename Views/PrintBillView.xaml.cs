﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaManagement.Views
{
    /// <summary>
    /// Interaction logic for PrintBillView.xaml
    /// </summary>
    public partial class PrintBillView : Window
    {
        public PrintBillView()
        {
            InitializeComponent();
        }
        private void btn_Print(object sender, RoutedEventArgs e)//Print
        {
            //Hàm in ra hóa đơn
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Print, "Invoice");
                    this.Close();
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
