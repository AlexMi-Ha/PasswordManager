using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// View Model for the Search dialog
    /// </summary>
    public class DialogSearchBoxViewModel : BaseViewModel {

        #region Private Attributes
        /// <summary>
        /// Attribute if the equals option is on
        /// </summary>
        private bool _equalsOption = false;
        /// <summary>
        /// Attribute if the contains option is on
        /// </summary>
        private bool _containsOption = true;

        #endregion

        #region Public Properties
        /// <summary>
        /// Was this dialog successful?
        /// </summary>
        public bool Successful { get; set; } = false;

        /// <summary>
        /// Text to search the database for
        /// </summary>
        public string SearchForText { get; set; }

        /// <summary>
        /// Do we search for the account name?
        /// </summary>
        public bool AccountNameSearch { get; set; } = true;

        /// <summary>
        /// Do we search for email?
        /// </summary>
        public bool EmailSearch { get; set; }

        /// <summary>
        /// Do we search for username?
        /// </summary>
        public bool UsernameSearch { get; set; }

        /// <summary>
        /// Do we search for the website?
        /// </summary>
        public bool WebsiteSearch { get; set; }

        
        /// <summary>
        /// Does the search text have to equal the database entry?
        /// </summary>
        public bool EqualsOption {
            get => _equalsOption;
            set {
                // only one option can be activated at a time
                _equalsOption = value;
                _containsOption = !value;
            }
        }

        public bool ContainsOption {
            get => _containsOption;
            set {
                // only one option can be activated at a time
                _containsOption = value;
                _equalsOption = !value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command when the user wants to search with the entered specifications
        /// </summary>
        public ICommand SearchCommand { get; set; }
        /// <summary>
        /// Command to cancel the operation
        /// </summary>
        public ICommand CancelCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DialogSearchBoxViewModel() {
            SearchCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Search(parameter));
            CancelCommand = new RelayParameterizedCommand<ICloseable>((parameter) => Cancel(parameter));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finish the search
        /// </summary>
        /// <param name="window"></param>
        private void Search(ICloseable window) {
            // this dialog was successful
            Successful = true;
            // close the dialog
            window.Close();
        }

        /// <summary>
        /// Cancel this dialog
        /// </summary>
        /// <param name="window"></param>
        private void Cancel(ICloseable window) {
            // unsuccessful result
            Successful = false;
            // close the dialog
            window.Close();
        }

        #endregion

    }
}
