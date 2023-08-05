using PasswordManager.Core.Helpers.Expressions;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PasswordManager.Core.ViewModel.Base {

    /// <summary>
    /// Base class for all ViewModels
    /// This Automatically handles all Property Changed Updates
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call to fire a PropertyChanged event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name) {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        /// <summary>
        /// Runs a command if the updating flag is not set
        /// Once the Action finishes the flag is reset
        /// </summary>
        /// <param name="updatingFlag">The flag indicating if this command is running</param>
        /// <param name="action">The action to run</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action) {
            // Check if the flag property is true
            if(updatingFlag.GetPropertyValue())
                return;

            // set the flag to true to show that this is running
            updatingFlag.SetPropertyValue(true);

            try {
                // Run the action
                await action();
            }finally {
                // set the flag back to false
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
