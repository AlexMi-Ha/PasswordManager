
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {

    /// <summary>
    /// The NoFrameHistoryProperty for creating a frame that never shows the navigation bar and keeps the navigation history empty
    /// </summary>
    public class NoFrameHistoryProperty : BaseAttachedProperty<NoFrameHistoryProperty, bool> {

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            
            // get the frame
            var frame = (sender as Frame);

            // Hide the navigation bar
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

            //clear the history
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();

        }
    }
}
