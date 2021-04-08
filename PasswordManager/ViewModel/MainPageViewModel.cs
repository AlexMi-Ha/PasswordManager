﻿using System.Windows.Input;

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
        public int PageHeaderHeight { get; set; } = 30;

        /// <summary>
        /// Width of the Button Side Panel
        /// </summary>
        public int ButtonPanelWidth { get; set; } = 240;

        /// <summary>
        /// Height of the Statusbar at the bottom of the UI
        /// </summary>
        public int StatusBarHeight { get; set; } = 22;
        #endregion

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
            SearchPasswordCommand = new RelayCommand(() => { });
            AddPasswordCommand = new RelayCommand(() => { });
            AccountButtonCommand = new RelayCommand(() => { });

        }
        #endregion

    }
}