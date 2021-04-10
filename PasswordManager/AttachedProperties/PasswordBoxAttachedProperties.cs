
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {

    /// <summary>
    /// The MonitorPassword Attached property for a password box
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool> {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            // get the caller
            var pwBox = sender as PasswordBox;
            
            // is it really a pw Box?
            if (pwBox == null)
                return;

            // Remove any previous events
            pwBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // if the caller set MonitorPassword to true, start listening out
            if((bool)e.NewValue) {

                //set default value
                HasTextProperty.SetValue(pwBox);

                // start listening
                pwBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Fired when the password value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            // set the attached property value
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// The HasText attached property for a password box
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool> {

        /// <summary>
        /// Sets the HasText Property based on if the caller has any text
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender) {
            HasTextProperty.SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
}
