using Dna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
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

        public ObservableCollection<PasswordListItemViewModel> Accounts { get; set; }

        /// <summary>
        /// Indicats if the refresh command is currently running
        /// </summary>
        public bool RefreshIsRunning { get; set; }

        #region Commands

        /// <summary>
        /// Command when the refresh button is pressed
        /// </summary>
        public ICommand RefreshCommand { get; set; }

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
        public ICommand LogoutButtonCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainPageViewModel() {

            // Initialize the Commands
            RefreshCommand = new RelayCommand(async () => await RunCommandAsync(
                    () => this.RefreshIsRunning, 
                    async () => Accounts = new ObservableCollection<PasswordListItemViewModel>(await GetUserContent())
                )
            );

            SearchPasswordCommand = new RelayCommand(async () => await SearchPassword());
            AddPasswordCommand = new RelayCommand(async () => await AddPassword());
            LogoutButtonCommand = new RelayCommand(Logout);

            // Retrieve the users accounts
            Task.Run(async () => Accounts = new ObservableCollection<PasswordListItemViewModel>( await GetUserContent()));
        }
        #endregion

        #region Methods


        private void Logout() {
            IoC.ApplicationViewModel.HandleLogout();
        }

        /// <summary>
        /// Retrieve all user account from the server
        /// </summary>
        /// <returns></returns>
        private async Task<List<PasswordListItemViewModel>> GetUserContent() {
            // get the info from the database
            GetUserContentDataModel result = await IoC.ClientDataStore.GetUserContentAsync(
                IoC.ApplicationViewModel.RunningLoginInfo
                );

            // was there an error? if yes display it
            if (result == null) {
                await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Failed to load User Content" }, "Error");
                return new List<PasswordListItemViewModel>();
            }

            // return the retrieved data
            return result.UserContent.Select(e => new PasswordListItemViewModel {
                Id = e.Id,
                AccountName = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.AccountNameHash),
                Email = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.EmailHash),
                Password = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.PasswordHash),
                Username = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.UsernameHash),
                Website = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.WebsiteHash),
                Notes = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.NotesHash)
            }).ToList();
        }

        /// <summary>
        /// Method to Filter or search for specific passwords
        /// </summary>
        /// <returns></returns>
        private async Task SearchPassword() {
            // create the dialog
            DialogSearchBoxViewModel viewModel = new DialogSearchBoxViewModel();
            await IoC.UI.SearchDialog(viewModel, "Search Password");

            // return if the dialog was unsuccessful
            if(!viewModel.Successful) {
                return;
            }

            // unchecked the user all the search boxes?
            if(!viewModel.AccountNameSearch && !viewModel.EmailSearch && !viewModel.UsernameSearch && !viewModel.WebsiteSearch) {
                // there can be no results, so clear the accounts and return
                Accounts.Clear();
                return;
            }
            // get usercontent from the server
            var userContent = await GetUserContent();

            // if searching for nothing -> just display all entries
            if(string.IsNullOrWhiteSpace(viewModel.SearchForText)) {
                Accounts = new ObservableCollection<PasswordListItemViewModel>(userContent);
                return;
            }

            var searchContent = new List<PasswordListItemViewModel>();
            // Search all valid entries
            if(viewModel.ContainsOption) {
                if (viewModel.AccountNameSearch) searchContent.AddRange(userContent.Where(e => e.AccountName.ToUpper().Contains(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.EmailSearch) searchContent.AddRange(userContent.Where(e => e.Email.ToUpper().Contains(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.UsernameSearch) searchContent.AddRange(userContent.Where(e => e.Username.ToUpper().Contains(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.WebsiteSearch) searchContent.AddRange(userContent.Where(e => e.Website.ToUpper().Contains(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
            } else {
                if (viewModel.AccountNameSearch) searchContent.AddRange(userContent.Where(e => e.AccountName.ToUpper().Equals(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.EmailSearch) searchContent.AddRange(userContent.Where(e => e.Email.ToUpper().Equals(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.UsernameSearch) searchContent.AddRange(userContent.Where(e => e.Username.ToUpper().Equals(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
                if (viewModel.WebsiteSearch) searchContent.AddRange(userContent.Where(e => e.Website.ToUpper().Equals(viewModel.SearchForText.ToUpper())).Where(e => !searchContent.Contains(e)));
            }

            // update the accounts list
            Accounts = new ObservableCollection<PasswordListItemViewModel>(searchContent);
        }

        /// <summary>
        /// Method to add a new password
        /// </summary>
        /// <returns></returns>
        private async Task AddPassword() {
            DialogPasswordItemViewModel viewModel = new DialogPasswordItemViewModel();
            await IoC.UI.ShowModifyDialog(viewModel, "Add Password");

            // return if the modification was canceled
            if(!viewModel.Success) {
                return;
            }

            // Call the database
            UserContentDataModel result = await IoC.ClientDataStore.AddUserContentAsync(
                IoC.ApplicationViewModel.RunningLoginInfo,
                new UserContentDataModel {
                    Id = Guid.NewGuid().ToString("N"),
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
                await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Failed to add Password" }, "Error");
                // done
                return;
            }

            // add the new Account to the view
            Accounts.Add(new PasswordListItemViewModel {
                Id = result.Id,
                AccountName = viewModel.AccountName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Username = viewModel.Username,
                Website = viewModel.Website,
                Notes = viewModel.Notes,
            });
        }


        #endregion

    }
}
