using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class AccountViewModel:BaseViewModel
    {
        private string _username;
        public string username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string _selectedGender;
        public string SelectedGender { get => _selectedGender; set { _selectedGender = value; OnPropertyChanged(); } }

        public string _hoten;
        public string hoten { get => _hoten; set { _hoten = value; OnPropertyChanged(); } }

        private DateTime _ngaysinh;
        public DateTime ngaysinh { get => _ngaysinh; set { _ngaysinh = value; OnPropertyChanged(); } }

        public string _sdt;
        public string sdt { get => _sdt; set { _sdt = value; OnPropertyChanged(); } }

        public string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }

        public string _diachi;
        public string diachi { get => _diachi; set { _diachi = value; OnPropertyChanged(); } }

        public ICommand UpdateImfomation { get; set; }
        public ICommand ChangePassword { get; set; }

        public AccountViewModel()
        {
            var list = DataProvider.Ins.DB.ACCOUNTs;
            foreach (var account in list)
            {
                if (account.IsLogin)
                {
                    username = account.A_USERNAME;
                    email = account.A_EMAIL;
                    sdt = account.A_PHONE;
                    diachi = account.A_ADDRESS;
                    ngaysinh = account.A_BDAY;
                    hoten = account.A_DISPLAYNAME;
                    SelectedGender = account.A_GENDER;
                }
            }

            UpdateImfomation = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(ngaysinh.ToString()) || string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(SelectedGender))
                {
                    return false;
                }

                var displaylist = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_DISPLAYNAME == hoten && x.A_ADDRESS == diachi && x.A_GENDER == SelectedGender && x.A_BDAY == ngaysinh && x.A_PHONE == sdt && x.A_EMAIL == email);
                if (displaylist == null || displaylist.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) => 
            {
                var acc = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_USERNAME == username).SingleOrDefault();
                acc.A_EMAIL = email;
                acc.A_DISPLAYNAME = hoten;
                acc.A_ADDRESS = diachi;
                acc.A_BDAY = ngaysinh;
                acc.A_PHONE = sdt;
                acc.A_GENDER = SelectedGender;

                DataProvider.Ins.DB.SaveChanges();
                MessageBoxCustom m = new MessageBoxCustom("Cập nhật thành công!", MessageType.Info, MessageButtons.Ok);
                m.ShowDialog();
            });

            ChangePassword = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ChangePasswordViewModel viewModel = new ChangePasswordViewModel(username);
                ChangePasswordView wd = new ChangePasswordView();
                wd.DataContext = viewModel;
                wd.ShowDialog();
            });
        }
    }
}
