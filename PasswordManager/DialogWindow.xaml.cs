using System.Windows;

namespace PasswordManager {
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window {

        #region Private Attributes
        /// <summary>
        /// View model for this window
        /// </summary>
        private DialogWindowViewModel _viewModel;
        #endregion

        #region Public Properties
        /// <summary>
        /// The viewmodel for this window
        /// </summary>
        public DialogWindowViewModel ViewModel {
            get => _viewModel;
            set {
                // set the new view model
                _viewModel = value;

                // update data context
                DataContext = _viewModel;
            }
        }
        #endregion


        #region Constructor

        public DialogWindow() {
            InitializeComponent();
        }

        #endregion
    }
}
