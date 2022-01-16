using Dna;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// View model for the PasswordList Item
    /// </summary>
    public class PasswordListItemViewModel : PasswordItemViewModel {

        private MainPageViewModel mainVm;

        #region Commands
        public ICommand CopyPasswordCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordListItemViewModel(MainPageViewModel mainVm) {
            this.mainVm = mainVm;
            // Initialize the Commands
            EditAccountCommand = new RelayCommand(async () => await EditAccount());
            DeleteAccountCommand = new RelayCommand(async () => await DeleteAccount());
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
                Username = this.Username,
                Website = this.Website,
                Notes = this.Notes,
            };
            await IoC.UI.ShowModifyDialog(viewModel, "Edit Account");

            // return if the modification was canceled
            if (!viewModel.Success) {
                return;
            }

            // Call the database
            UserContentDataModel result = await IoC.ClientDataStore.UpdateUserContentAsync(
                IoC.ApplicationViewModel.RunningLoginInfo,
                new UserContentDataModel {
                    Id = viewModel.Id,
                    AccountNameHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.AccountName),
                    EmailHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Email),
                    PasswordHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Password),
                    UsernameHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Username),
                    WebsiteHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Website),
                    NotesHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Notes),
                }
                );

            // if the response has an error -> display it
            if (result == null) {
                await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Failed to edit the Account" }, "Error");
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
        private async Task DeleteAccount() {
            // TODO
            var vm = new DialogChoiceBoxViewModel { Message = "Do you really want to delete this account?" };
            await IoC.UI.ShowChoiceBoxDialog(vm, "Delete");

            if(vm.DialogResult == false) {
                return;
            }
            bool result = await IoC.ClientDataStore.RemoveUserContentAsync(
                IoC.ApplicationViewModel.RunningLoginInfo,
                Id
            );

            if(result) {
                mainVm.Accounts.Remove(this);
            }
        }

        #endregion
    }
}
