using PasswordManager.Core.Common.DataModels.Database;
using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.ViewModel.Base;
using PasswordManager.Core.ViewModel.Util.Base;
using PasswordManager.Core.ViewModel.Util.Dialogs;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Util {
    /// <summary>
    /// View model for the PasswordList Item
    /// </summary>
    public class PasswordListItemViewModel : PasswordItemViewModel {

        private MainPageViewModel mainVm;

        public ICommand CopyPasswordCommand { get; set; }
        public ICommand EditAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }

        public PasswordListItemViewModel(MainPageViewModel mainVm) {
            this.mainVm = mainVm;
            // Initialize the Commands
            EditAccountCommand = new RelayCommand(async () => await EditAccount());
            DeleteAccountCommand = new RelayCommand(async () => await DeleteAccount());
            CopyPasswordCommand = new RelayParameterizedCommand<ICopyClipboard>((parameter) => CopyPassword(parameter));

        }

        private void CopyPassword(ICopyClipboard parameter) {
            parameter.CopyToClipboard(Password);
        }

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

    }
}
