using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel() 
        { 
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Login(p);
            });
        }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }

            IsLogin = true;
            p.Close();
        }
    }
}
