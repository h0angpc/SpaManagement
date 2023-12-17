using SpaManagement.Commands;
using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class MainViewModel : BaseViewModel 
    {
        private BaseViewModel _selectedViewModel = new HomeViewModel();

        public BaseViewModel  SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value; 
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public ICommand LogOutCommand { get; set; }



        public bool IsLoaded = false;
        public MainViewModel() 
        {
            UpdateViewCommand = new UpdateViewCommand(this);

            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                bool? result = new MessageBoxCustom("Xác nhận đăng xuất", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (result.Value)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != p)
                        {
                            window.Close();
                        }
                    }
                    LoginWindow w = new LoginWindow();
                    p.Close();
                    w.Show();
                }
                else
                {
                    return;
                }
            }
            );
        }
   }
}
