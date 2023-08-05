using System;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Base {
    /// <summary>
    /// Relay Command to run an action
    /// </summary>
    public class RelayCommand : ICommand {


        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// The action to run
        /// </summary>
        private Action action;

        public RelayCommand(Action action) {
            this.action = action;
        }

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
            action();
        }
    }
}
