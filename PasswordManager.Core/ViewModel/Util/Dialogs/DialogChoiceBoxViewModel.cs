using System;
using System.Windows.Input;

namespace PasswordManager.Core {

    /// <summary>
    /// View Model for the Message Dialog Box
    /// </summary>
    public class DialogMessageBoxViewModel :  BaseViewModel {

        #region Public Properties
        /// <summary>
        /// Message for the messagebox dialog
        /// </summary>
        public string Message { get; set; }
        #endregion


        #region Commands

        /// <summary>
        /// Command when the ok button is pressed
        /// </summary>
        public ICommand OkCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DialogMessageBoxViewModel() {
            // Initialize Commands
            OkCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Ok(parameter));
        }

        #endregion


        #region Methods
        /// <summary>
        /// Methods for when the Dialog Ok button is pressed
        /// </summary>
        /// <param name="parameter"></param>
        private void Ok(ICloseable parameter) {
            // Close this dialog
            parameter.Close();
        }
        #endregion

    }
}
