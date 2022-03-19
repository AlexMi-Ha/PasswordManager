using PasswordManager.Core;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager {
    /// <summary>
    /// Interaktionslogik für RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterPageViewModel>, IHavePassword {
        public RegisterPage() {
            InitializeComponent();
            EmailText.Focus();
        }

        public List<SecureString> SecurePassword => new List<SecureString> { PasswordText.SecurePassword, PasswordTextRepeat.SecurePassword };
    }
}
