using PropertyChanged;
using System.ComponentModel;

namespace PasswordManager {

    /// <summary>
    /// Base class for all ViewModels
    /// This Automatically handles all Property Changed Updates
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call to fire a PropertyChanged event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name) {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
