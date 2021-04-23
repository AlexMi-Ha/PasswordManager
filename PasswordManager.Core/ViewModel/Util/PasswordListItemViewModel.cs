﻿using Dna;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// View model for the PasswordList Item
    /// </summary>
    public class PasswordListItemViewModel : PasswordItemViewModel {


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
            EditAccountCommand = new RelayCommand(async () => await EditAccount());
            DeleteAccountCommand = new RelayCommand(DeleteAccount);
            CopyPasswordCommand = new RelayParameterizedCommand<ICopyClipboard>((parameter) => CopyPassword(parameter));

        }
        #endregion

        #region Methods

        /// <summary>
        /// Tell the ui to copy the password to clipboard
        /// UI handles it as it is operating system specific
        /// </summary>
        /// <param name="parameter"></param>
        private void CopyPassword(ICopyClipboard parameter) {
            parameter.CopyToClipboard(Password);
        }

        /// <summary>
        /// Edit this account
        /// </summary>
        private async Task EditAccount() {
            // Fill the view Model with the information
            DialogPasswordItemViewModel viewModel = new DialogPasswordItemViewModel {
                Id = this.Id,
                AccountName = this.AccountName,
                Email = this.Email,
                Password = this.Password,
                Username = this.Password,
                Website = this.Website,
                Notes = this.Notes,
            };
            await IoC.UI.ShowModifyDialog(viewModel, "Edit Account");

            // return if the modification was canceled
            if (!viewModel.Success) {
                return;
            }

            // Call the server
            var result = await WebRequests.PostAsync<ApiResponse<UserContentApiModel>>(
               ApiRoutes.ServerAdress + ApiRoutes.UpdateUserContent,
                new UserContentApiModel {
                    Id = viewModel.Id,
                    // TODO encrypt info and send to server       
                });

            // if the response has an error -> display it
            if (await result.DisplayErrorIfFailedAsync("Failed to edit the Account")) {
                // done
                return;
            }

            // update the values in this ViewModel
            Id = viewModel.Id;
            AccountName = viewModel.AccountName;
            Email = viewModel.Email;
            Password = viewModel.Password;
            Username = viewModel.Username;
            Website = viewModel.Website;
            Notes = viewModel.Notes;
        }

        /// <summary>
        /// Delete this account
        /// </summary>
        private void DeleteAccount() {
            // TODO
        }

        #endregion
    }
}
