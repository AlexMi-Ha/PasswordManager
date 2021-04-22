
using PasswordManager.Core;

namespace PasswordManager {
    /// <summary>
    /// Locates view models from the IoC for use in Binding in the views
    /// </summary>
    public class ViewModelLocator {

        #region Public Properties
        /// <summary>
        /// Singleton Instance of the Locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();
        #endregion

        #region ViewModels
        /// <summary>
        /// The Application View Model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        #endregion
    }
}
