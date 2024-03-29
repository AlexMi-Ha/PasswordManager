﻿using System;
using System.Windows.Input;

namespace PasswordManager.Core {

    /// <summary>
    /// View Model for the Message Dialog Box
    /// </summary>
    public class DialogChoiceBoxViewModel :  DialogMessageBoxViewModel {

        #region Public Properties

        /// <summary>
        /// Result of this dialog
        /// </summary>
        public bool DialogResult { get; private set; } = false;

        #endregion

        #region Commands

        /// <summary>
        /// Command when the cancel button is pressed
        /// </summary>
        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DialogChoiceBoxViewModel() {
            // Initialize Commands
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
            OkCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Ok(parameter));
        }

        #endregion


        #region Methods
        /// <summary>
        /// Methods for when the Dialog Cancel button is pressed
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(ICloseable parameter) {
            DialogResult = false;
            // Close this dialog
            parameter.Close();
        }

        /// <summary>
        /// Methods for when the Dialog Ok button is pressed
        /// </summary>
        /// <param name="parameter"></param>
        private void Ok(ICloseable parameter) {
            DialogResult = true;
            // Close this dialog
            parameter.Close();
        }
        #endregion

    }
}
