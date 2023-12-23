using SpaManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaManagement.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel=viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new HomeViewModel();
            }
            else if (parameter.ToString() == "Customer")
            {
                viewModel.SelectedViewModel = new CustomerViewModel();
            }
            else if (parameter.ToString() == "Payment")
            {
                viewModel.SelectedViewModel = new PaymentViewModel();
            }
            else if (parameter.ToString() == "Product")
            {
                viewModel.SelectedViewModel = new ProductViewModel();
            }
            else if (parameter.ToString() == "Employee")
            {
                viewModel.SelectedViewModel = new EmployeeViewModel();
            }
            else if (parameter.ToString() == "Account")
            {
                viewModel.SelectedViewModel = new AccountViewModel();
            }
            else if(parameter.ToString() == "Service")
            {
                viewModel.SelectedViewModel = new ServiceViewModel();
            }
        }
    }
}
