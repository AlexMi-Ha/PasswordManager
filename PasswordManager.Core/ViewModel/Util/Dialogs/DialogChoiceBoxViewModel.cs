using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.ViewModel.Base;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Util.Dialogs {

    /// <summary>
    /// View Model for the Message Dialog Box
    /// </summary>
    public class DialogMessageBoxViewModel :  BaseViewModel {

        public string Message { get; set; }


        public ICommand OkCommand { get; set; }

        public DialogMessageBoxViewModel() {
            // Initialize Commands
            OkCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Ok(parameter));
        }

        private void Ok(ICloseable parameter) {
            // Close this dialog
            parameter.Close();
        }
    }
}
