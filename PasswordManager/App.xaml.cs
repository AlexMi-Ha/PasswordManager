using PasswordManager.Core;
using System;
using System.Windows;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        /// <summary>
        /// Custom Startup so the IoC Container can be loaded
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            // Setup the main application
            ApplicationSetup();

            //Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup() {
            // Setup the IoC
            IoC.Setup();

            // Bind the UI Manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }
    }
}
