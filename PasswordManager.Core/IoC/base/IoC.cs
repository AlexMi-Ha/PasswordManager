using Ninject;

namespace PasswordManager.Core {

    /// <summary>
    /// The IoC Container of the application
    /// </summary>
    public static class IoC {

        /// <summary>
        /// The Kernal for the container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();


        #region Construction

        /// <summary>
        /// Sets up the container and binds all information required.
        /// Must be called as soon as the application starts up
        /// </summary>
        public static void Setup() {
            // Bind all required ViewModels
            BindViewModels();
        }

        /// <summary>
        /// Binds all Singleton Viewmodels
        /// </summary>
        private static void BindViewModels() {
            // Bind to a single instance of ApplicationViewModel
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Getter for a service from the IoC
        /// </summary>
        /// <typeparam name="T">Type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>() {
            return Kernel.Get<T>();
        }
        #endregion

    }
}
