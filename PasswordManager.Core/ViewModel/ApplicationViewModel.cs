
namespace PasswordManager.Core {
    /// <summary>
    /// ApplicationState as a ViewModel
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {


        #region Public Properties
        /// <summary>
        /// The current Page of the Application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.MainPage;

        #endregion
    }
}
