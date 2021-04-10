
using System.Security;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginPageViewModel>, IHavePassword {
        public LoginPage() {
            InitializeComponent();
            PasswordText.Focus();
        }

        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
