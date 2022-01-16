using Dna;
using PasswordManager.Core;
using PasswordManager.Data;
using System.Threading.Tasks;
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
        protected override async void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            // Setup the main application
            await ApplicationSetupAsync();

            //Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync() {

            Framework.Construct<DefaultFrameworkConstruction>()
                .UseClientDataStore()
                .Build();

            // Setup the IoC
            IoC.Setup();

            // Bind the UI Manager
            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());

            var a = Framework.Service<IClientDataStore>();

            // Ensure Data Store
            await IoC.ClientDataStore.EnsureDataStoreAsync();

        }
    }
}
