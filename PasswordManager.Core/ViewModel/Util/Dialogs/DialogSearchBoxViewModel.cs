using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.ViewModel.Base;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Util.Dialogs {
    /// <summary>
    /// View Model for the Search dialog
    /// </summary>
    public class DialogSearchBoxViewModel : BaseViewModel {

        private bool _equalsOption = false;
        private bool _containsOption = true;

        public bool Successful { get; set; } = false;
        public string SearchForText { get; set; }
        public bool AccountNameSearch { get; set; } = true;
        public bool EmailSearch { get; set; }
        public bool UsernameSearch { get; set; }
        public bool WebsiteSearch { get; set; }

        
        public bool EqualsOption {
            get => _equalsOption;
            set {
                // only one option can be activated at a time
                _equalsOption = value;
                _containsOption = !value;
            }
        }

        public bool ContainsOption {
            get => _containsOption;
            set {
                // only one option can be activated at a time
                _containsOption = value;
                _equalsOption = !value;
            }
        }
        
        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public DialogSearchBoxViewModel() {
            SearchCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Search(parameter));
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
        }

        private void Search(ICloseable window) {
            // this dialog was successful
            Successful = true;
            // close the dialog
            window.Close();
        }

        private void Cancel(ICloseable window) {
            // unsuccessful result
            Successful = false;
            // close the dialog
            window.Close();
        }

    }
}
