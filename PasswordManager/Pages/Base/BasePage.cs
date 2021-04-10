using PasswordManager.Core;
using System.Windows.Controls;

namespace PasswordManager {
    /// <summary>
    /// Base class for all Pages
    /// </summary>
    /// <typeparam name="VM">View Model this page is bound to</typeparam>
    public class BasePage<VM> : Page where VM : BaseViewModel, new(){

        #region Private Attributes
        /// <summary>
        /// View Model this page is bound to
        /// </summary>
        private VM _ViewModel;
        #endregion

        #region Public Properties

        /// <summary>
        /// ViewModel this page is bound to
        /// </summary>
        public VM ViewModel { 
            get { return _ViewModel; } 
            set {
                // has anything changed?
                if (_ViewModel == value)
                    return;

                // update the viewmodel
                _ViewModel = value;
                this.DataContext = _ViewModel;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Contructor
        /// </summary>
        public BasePage() {
            // Add the ViewModel
            this.ViewModel = new VM();
        }
        #endregion
    }
}
