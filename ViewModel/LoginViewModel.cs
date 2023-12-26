using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {

        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _PassWord;

        public string PassWord
        {
            get => _PassWord;
            set
            {
                _PassWord = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }
        public ICommand LoginCommand { get; set; }

        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ShowQuenMK {  get; set; }


        public LoginViewModel()
        {
            UserName = "";
            PassWord = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Login(p);
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PassWord = p.Password;
            });
            ShowQuenMK = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                QuenMKView wd = new QuenMKView();
                wd.ShowDialog();
            });
         }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }
            
            string passEncode = Base64Encode(PassWord);

            var accCount = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_USERNAME == UserName && x.A_PASSWORD == passEncode).Count();

            if (accCount > 0)
            {
                MainWindow mwindow = new MainWindow();
                foreach (var acc in DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_USERNAME== UserName && x.A_PASSWORD == passEncode))
                {
                    acc.IsLogin = true;
                }
                p.Close();
                mwindow.Show();
            }
            else
            {
                MessageBoxCustom m = new MessageBoxCustom("Đăng nhập thất bại!", MessageType.Error, MessageButtons.Ok);
                m.ShowDialog();
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
