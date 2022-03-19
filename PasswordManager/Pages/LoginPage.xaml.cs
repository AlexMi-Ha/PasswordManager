
using PasswordManager.Core;
using System.Collections.Generic;
using System.Security;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginPageViewModel>, IHavePassword {
        public LoginPage() {
            InitializeComponent();
            EmailText.Focus();
        }

        public List<SecureString> SecurePassword => new List<SecureString> { PasswordText.SecurePassword };
    }
}
