using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager {
    /// <summary>
    /// View Model for the main page of the application
    /// </summary>
    public class MainPageViewModel : BaseViewModel {

        #region Private Attributes

        #endregion

        #region Public Properties

        /// <summary>
        /// Height of the Page Header
        /// </summary>
        public int PageHeaderHeight { get; set; } = 0;

        /// <summary>
        /// Width of the Button Side Panel
        /// </summary>
        public int ButtonPanelWidth { get; set; } = 240;

        /// <summary>
        /// Height of the Statusbar at the bottom of the UI
        /// </summary>
        public int StatusBarHeight { get; set; } = 22;
        #endregion

        public ObservableCollection<PasswordListItem> Accounts { get; set; }

        #region Commands

        /// <summary>
        /// Command when the 'Search Password' button is pressed
        /// </summary>
        public ICommand SearchPasswordCommand { get; set; }

        /// <summary>
        /// Command when the 'Add Password' button is pressed
        /// </summary>
        public ICommand AddPasswordCommand { get; set; }

        /// <summary>
        /// Command when the 'Account' button is pressed
        /// </summary>
        public ICommand AccountButtonCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainPageViewModel() {

            // Initialize the Commands
            SearchPasswordCommand = new RelayCommand(() => { MessageBox.Show("TODO"); });
            AddPasswordCommand = new RelayCommand(() => { MessageBox.Show("TODO"); });
            AccountButtonCommand = new RelayCommand(() => { MessageBox.Show("TODO"); });

            Accounts = new ObservableCollection<PasswordListItem>();

            Accounts.Add(new PasswordListItem());
            Accounts.Add(new PasswordListItem());
            Accounts.Add(new PasswordListItem());
            Accounts.Add(new PasswordListItem());
            Accounts.Add(new PasswordListItem());
            Accounts.Add(new PasswordListItem());
        }
        #endregion

    }
}
