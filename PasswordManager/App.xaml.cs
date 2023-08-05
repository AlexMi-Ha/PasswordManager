using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Core.DI;
using System.Windows;

namespace PasswordManager {
    public partial class App : Application {

        private ServiceProvider _serviceProvider;

        public App() {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            DICollection.Setup(_serviceProvider);
        }

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            //Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        protected void ConfigureServices(IServiceCollection services) {
            
        }

    }
}
