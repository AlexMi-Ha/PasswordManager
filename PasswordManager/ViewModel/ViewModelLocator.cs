
using PasswordManager.Core.DI;
using PasswordManager.Core.ViewModel;

namespace PasswordManager {
    public class ViewModelLocator {

        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        public static ApplicationViewModel ApplicationViewModel => DICollection.Resolve<ApplicationViewModel>();

    }
}
