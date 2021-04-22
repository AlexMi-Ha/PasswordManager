using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// View model for the PasswordList Item
    /// </summary>
    public class PasswordListItemViewModel : PasswordItemViewModel {


        #region Commands
        public ICommand CopyPasswordCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordListItemViewModel() {
            // Initialize the Commands
            CopyPasswordCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });
            EditAccountCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });
            DeleteAccountCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });

        }
        #endregion
    }
}
