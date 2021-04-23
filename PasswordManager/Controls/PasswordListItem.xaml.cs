using PasswordManager.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for PasswordListItem.xaml
    /// </summary>
    public partial class PasswordListItem : UserControl, ICopyClipboard {
        public PasswordListItem() {
            InitializeComponent();
            // No Datacontext needed
            // Data will be bound to this Control in the MainPage
        }

        public void CopyToClipboard(string text) {
            Clipboard.SetText(text);
        }
    }
}
