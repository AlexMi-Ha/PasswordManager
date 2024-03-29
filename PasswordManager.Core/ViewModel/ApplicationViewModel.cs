﻿using System.Security;

namespace PasswordManager.Core {
    /// <summary>
    /// ApplicationState as a ViewModel
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {


        #region Public Properties
        /// <summary>
        /// The current Page of the Application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// Hash of the users master password. Used as a key for en/decryption
        /// </summary>
        public string MasterHash { get; set; }

        public LoginResultDataModel RunningLoginInfo { get; private set; }

        #endregion


        #region Public Methods

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">the page to go to</param>
        public void GoToPage(ApplicationPage page) {
            // set the page
            CurrentPage = page;
        }

        /// <summary>
        /// Handles what happens on a successful login
        /// </summary>
        public void HandleSuccessfulLogin(LoginResultDataModel loginResult) {
            RunningLoginInfo = loginResult;
            GoToPage(ApplicationPage.MainPage);
        }

        public void HandleLogout() {
            RunningLoginInfo = null;
            MasterHash = string.Empty;
            GoToPage(ApplicationPage.Login);
        }
        #endregion
    }
}
