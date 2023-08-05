using PasswordManager.Core.Common.Enums;
using PasswordManager.Core.ViewModel.Base;

namespace PasswordManager.Core.ViewModel {
    /// <summary>
    /// ApplicationState as a ViewModel
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {


        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        public string MasterHash { get; set; }

        public LoginResultDataModel RunningLoginInfo { get; private set; }



        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">the page to go to</param>
        public void GoToPage(ApplicationPage page) {
            // set the page
            CurrentPage = page;
        }

        public void HandleSuccessfulLogin(LoginResultDataModel loginResult) {
            RunningLoginInfo = loginResult;
            GoToPage(ApplicationPage.MainPage);
        }

        public void HandleLogout() {
            RunningLoginInfo = null;
            MasterHash = string.Empty;
            GoToPage(ApplicationPage.Login);
        }
    }
}
