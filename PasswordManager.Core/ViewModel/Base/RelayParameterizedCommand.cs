﻿using System;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// Relay Command to run an action
    /// </summary>
    public class RelayParameterizedCommand<T> : ICommand {

        #region Public Events

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Private Attributes
        /// <summary>
        /// The action to run
        /// </summary>
        private Action<T> action;
        #endregion

        #region Constructor
        public RelayParameterizedCommand(Action<T> action) {
            this.action = action;
        }
        #endregion

        #region Command Methods
        /// <summary>
        /// RelayCommands can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) {
            return true;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) {
            action((T)parameter);
        }
        #endregion
    }
}
