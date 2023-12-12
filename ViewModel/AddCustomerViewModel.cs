using SpaManagement.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaManagement.ViewModel
{
    public class AddCustomerViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly ErrorsViewModel _errorsViewModel;

        private string _name;

		public string Name
		{
			get 
			{
				return _name; 
			}
			set 
			{ 
				_name = value; 
				OnPropertyChanged(nameof(Name));
			}
		}

        private string _email;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _phone;

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;

                if (_phone == "abc")
                {
                    
                }

                OnPropertyChanged(nameof(Phone));
            }
        }
        public bool CanCreate => !HasErrors;

        public ICommand AddCustomerCommand { get; }

        public bool HasErrors => _errorsViewModel.HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public AddCustomerViewModel()
        {
            AddCustomerCommand = new AddCustomerCommand(this);

            _errorsViewModel = new ErrorsViewModel();
            _errorsViewModel.ErrorsChanged += ErrorsViewModel_ErrorsChanged;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsViewModel.GetErrors(propertyName);
        }

        private void ErrorsViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(CanCreate));
        }

    }
}
