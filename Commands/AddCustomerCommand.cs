using SpaManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SpaManagement.Commands
{
    public class AddCustomerCommand : ICommand
    {
        private readonly AddCustomerViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public AddCustomerCommand(AddCustomerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            MessageBox.Show($"Successfully created '{_viewModel.Name}' for {_viewModel.Phone:C}.");
        }
    }
}
