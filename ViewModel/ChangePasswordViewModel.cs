using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Security.Cryptography;
using SpaManagement.Model;
using SpaManagement.Views;
using System.ComponentModel;
using System.Collections;
using System.Security.Policy;

namespace SpaManagement.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private string _ErrorText;

        public string ErrorText
        {
            get { return _ErrorText; }
            set { _ErrorText = value; OnPropertyChanged(nameof(ErrorText)); }
        }

        private Visibility _errorVisibility;
        public Visibility ErrorVisibility
        {
            get { return _errorVisibility; }
            set
            {
                if (_errorVisibility != value)
                {
                    _errorVisibility = value;
                    OnPropertyChanged(nameof(ErrorVisibility));
                }
            }
        }

        private string _password1;
        public string password1 
        {
            get => _password1; 
            set {
                _password1 = value;
                OnPropertyChanged(nameof(password1)); 
            } 
        }
        private string _password2;
        public string password2 
        {
            get => _password2; 
            set 
            { 
                _password2 = value; 
                OnPropertyChanged(nameof(password2)); 
            } 
        }
        private string _password3;
        public string password3 
        { 
            get => _password3; 
            set 
            {
                _password3 = value;

                OnPropertyChanged(nameof(password3)); 
            } 
        }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand1 { get; set; }
        public ICommand PasswordChangedCommand2 { get; set; }
        public ICommand PasswordChangedCommand3 { get; set; }
        public ICommand SaveCommand { get; set; }


        public ChangePasswordViewModel(string username)
        {
            PasswordChangedCommand1 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { password1 = p.Password; OnPropertyChanged(); });
            PasswordChangedCommand2 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { password2 = p.Password; OnPropertyChanged(); });
            PasswordChangedCommand3 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => 
            {
                password3 = p.Password;
                if (password3 != password2)
                {
                    ErrorText = "Mật khẩu xác nhận không chính xác";
                    ErrorVisibility = Visibility.Visible;
                }
                else
                {
                    ErrorVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged(); 
            });

            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            SaveCommand = new RelayCommand<Window>((p) => 
            {
                if (password1 == "" || password2 == "" || password3 =="")
                {
                    return false;
                }

                if (password1 == password2)
                    return false;
                if (password2 != password3)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    string pass1 = Base64Encode(password1);
                    var acc = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_USERNAME == username && x.A_PASSWORD == pass1).SingleOrDefault();
                    if (acc != null)
                    {
                        if (password2 == password3)
                        {
                            string Password3 = Base64Encode(password3);
                            acc.A_PASSWORD = Password3;
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBoxCustom m = new MessageBoxCustom("Đổi mật khẩu thành công!", MessageType.Info, MessageButtons.Ok);
                            m.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBoxCustom m = new MessageBoxCustom("Mật khẩu cũ không chính xác!", MessageType.Error, MessageButtons.Ok);
                        m.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxCustom m = new MessageBoxCustom("Mật khẩu cũ không chính xác!", MessageType.Error, MessageButtons.Ok);
                    m.ShowDialog();
                }
            });

        }

        public static string Base64Encode(string plainText)
        {
            if (plainText == null)
            {
                return string.Empty;
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
