using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// Viewmodel for a PasswordItem 
    /// </summary>
    public class PasswordItemViewModel : BaseViewModel {

        #region Public Properties

        /// <summary>
        /// ID of this account in the database
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Name of this AccountItem
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Email of this AccountItem
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Username of this AccountItem
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Website this Account is used on
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Password of this Account
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Notes about this account
        /// </summary>
        public string Notes { get; set; }
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordItemViewModel() {

        }
        #endregion
    }
}
