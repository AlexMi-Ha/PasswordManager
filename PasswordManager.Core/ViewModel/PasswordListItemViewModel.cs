﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager.Core {
    public class PasswordListItemViewModel : BaseViewModel {

        #region Public Properties

        /// <summary>
        /// ID of this account in the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The Name of this AccountItem
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Email of this AccountItem
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Username of this AccountItem
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Website this Account is used on
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Notes about this account
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Date this Account was created
        /// </summary>
        public DateTime DateCreated { get; set; }
        #endregion

        #region Commands
        public ICommand CopyPasswordCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordListItemViewModel() {
            // Initialize the Commands
            CopyPasswordCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });
            EditAccountCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });
            DeleteAccountCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });

        }
        #endregion
    }
}