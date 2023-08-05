using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.ViewModel.Base;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Util.Dialogs {

    /// <summary>
    /// View Model for the Message Dialog Box
    /// </summary>
    public class DialogChoiceBoxViewModel :  DialogMessageBoxViewModel {

        public bool DialogResult { get; private set; } = false;

        public ICommand CancelCommand { get; set; }

        public DialogChoiceBoxViewModel() {
            // Initialize Commands
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
            OkCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Ok(parameter));
        }


        private void Cancel(ICloseable parameter) {
            DialogResult = false;
            // Close this dialog
            parameter.Close();
        }

        private void Ok(ICloseable parameter) {
            DialogResult = true;
            // Close this dialog
            parameter.Close();
        }
    }
}
