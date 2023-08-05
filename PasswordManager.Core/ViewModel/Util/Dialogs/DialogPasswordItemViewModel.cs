using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.Security;
using PasswordManager.Core.ViewModel.Base;
using PasswordManager.Core.ViewModel.Util.Base;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Util.Dialogs {

    /// <summary>
    /// Viewmodel for the edit dialog
    /// </summary>
    public class DialogPasswordItemViewModel : PasswordItemViewModel {

        public bool Success { get; set; } = false;

        public ICommand DoneCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand GeneratePasswordCommand { get; set; }

        public DialogPasswordItemViewModel() {
            // Initialize the commands
            DoneCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Done(parameter));
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
            GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        }

        private void Done(ICloseable window) {
            // Make sure the user entered all must have information
            if(string.IsNullOrWhiteSpace(AccountName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password)) {
                return;
            }
            // dialog was successful
            Success = true;
            // close the dialog
            window.Close();
        }

        private void Cancel(ICloseable window) {
            // Dialog was unsuccessful
            Success = false;
            // close the dialog
            window.Close();
        }

        private void GeneratePassword() {
            // Todo: get settings from the settings db
            var includeLowercase = true;
            var includeUppercase = true;
            var includeNum = true;
            var includeSpecial = true;
            var disableTwoIdenticalsInARow = true;
            var minLengthPassword = 15;
            var maxLengthPassword = 20;
            Password = Crypt.GeneratePassword(includeLowercase, includeUppercase, includeNum, includeSpecial, disableTwoIdenticalsInARow, minLengthPassword, maxLengthPassword);
        }
    }
}
