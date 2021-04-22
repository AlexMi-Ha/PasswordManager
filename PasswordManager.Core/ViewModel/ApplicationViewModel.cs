
using System;
using System.Threading.Tasks;

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
        public void HandleSuccessfulLogin(LoginResultApiModel loginResult) {
            GoToPage(ApplicationPage.MainPage);
        }
        #endregion
    }
}
