using System.Windows.Input;

namespace PasswordManager.Core {

    /// <summary>
    /// Viewmodel for the edit dialog
    /// </summary>
    public class DialogPasswordItemViewModel : PasswordItemViewModel {

        #region Public Properties
        /// <summary>
        /// Bool if the Dialog was successful (exited with a successful 'done' command)
        /// </summary>
        public bool Success { get; set; } = false;
        #endregion

        #region Commands
        /// <summary>
        /// Command when the done button is pressed
        /// </summary>
        public ICommand DoneCommand { get; set; }
        /// <summary>
        /// Command when the cancel button is pressed
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Command executed when a new Password should be generated
        /// </summary>
        public ICommand GeneratePasswordCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">ViewModel of the Main page to save the current state when entering the dialog page</param>
        public DialogPasswordItemViewModel() {
            // Initialize the commands
            DoneCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Done(parameter));
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
            GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        }

        #endregion

        #region Methods

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

        /// <summary>
        /// Method when the Cancel button was clicked
        /// Cancels the modification
        /// </summary>
        private void Cancel(ICloseable window) {
            // Dialog was unsuccessful
            Success = false;
            // close the dialog
            window.Close();
        }

        private void GeneratePassword() {
            // TODO
            Password = "TODO";
        }

        #endregion

    }
}
