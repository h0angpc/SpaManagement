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

        public ICommand LoadedWindowCommand { get; set; }

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
         }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }

            string passEncode = MD5Hash(Base64Encode(PassWord));

            var accCount = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_USERNAME == UserName && x.A_PASSWORD == passEncode).Count();

            if (accCount > 0)
            {
                MainWindow mwindow = new MainWindow();
                //MessageBoxCustom m = new MessageBoxCustom("Đăng nhập thành công!", MessageType.Info, MessageButtons.Ok);
                //m.ShowDialog();
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

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
