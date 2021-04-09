using System.Windows.Controls;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for PasswordListItem.xaml
    /// </summary>
    public partial class PasswordListItem : UserControl {
        public PasswordListItem() {
            InitializeComponent();
            // No Datacontext needed
            // Data will be bound to this Control in the MainPage
        }
    }
}
