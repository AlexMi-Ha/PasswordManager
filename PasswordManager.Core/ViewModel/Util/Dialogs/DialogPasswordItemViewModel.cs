using System.Windows.Input;

namespace PasswordManager.Core {

    /// <summary>
    /// Viewmodel for the edit dialog
    /// </summary>
    public class DialogPasswordItemViewModel : PasswordItemViewModel {

        #region Commands
        /// <summary>
        /// Command when the done button is pressed
        /// </summary>
        public ICommand DoneCommand;
        /// <summary>
        /// Command when the cancel button is pressed
        /// </summary>
        public ICommand CancelCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">ViewModel of the Main page to save the current state when entering the dialog page</param>
        public DialogPasswordItemViewModel(MainPageViewModel model) {
            CancelCommand = new RelayCommand();
        }

        #endregion

    }
}
