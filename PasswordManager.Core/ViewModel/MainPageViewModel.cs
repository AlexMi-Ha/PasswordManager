using Dna;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            SearchPasswordCommand = new RelayCommand(async () => await SearchPassword());
            AddPasswordCommand = new RelayCommand(async () => await AddPassword());
            AccountButtonCommand = new RelayCommand(() => { Console.WriteLine("TODO"); });

            // Retrieve the users accounts
            Task.Run(async () => Accounts = await GetUserContent());

        }
        #endregion

        #region Methods

        /// <summary>
        /// Retrieve all user account from the server
        /// </summary>
        /// <returns></returns>
        private async Task<ObservableCollection<PasswordListItemViewModel>> GetUserContent() {
            // get the info from the server
            var result = await WebRequests.PostAsync<ApiResponse<GetUserContentApiModel>>(
                    ApiRoutes.ServerAdress + ApiRoutes.GetUserContent,
                    bearerToken: IoC.ApplicationViewModel.ClientToken
                );

            // was there an error? if yes display it
            if (await result.DisplayErrorIfFailedAsync("Failed to load User Content")) {
                return new ObservableCollection<PasswordListItemViewModel>();
            }

            // return the retrieved data
            return new ObservableCollection<PasswordListItemViewModel>( 
                result.ServerResponse.Response.UserContent.Select(e => new PasswordListItemViewModel {
                Id = e.Id,
                AccountName = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.AccountNameHash),
                Email = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.EmailHash),
                Password = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.PasswordHash),
                Username = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.UsernameHash),
                Website = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.WebsiteHash),
                Notes = Crypt.DecryptString(IoC.ApplicationViewModel.MasterHash, e.NotesHash)
            }));
        }

        /// <summary>
        /// Method to Filter or search for specific passwords
        /// </summary>
        /// <returns></returns>
        private async Task SearchPassword() {
            await Task.Delay(1);
            Console.WriteLine("TODO");
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

            // Call the server
            var result = await WebRequests.PostAsync<ApiResponse<UserContentApiModel>>(
               ApiRoutes.ServerAdress + ApiRoutes.AddUserContent,
                new UserContentApiModel {
                    Id = Guid.NewGuid().ToString("N"),
                    AccountNameHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.AccountName),
                    EmailHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Email),
                    PasswordHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Password),
                    UsernameHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Username),
                    WebsiteHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Website),
                    NotesHash = Crypt.EncryptString(IoC.ApplicationViewModel.MasterHash, viewModel.Notes),
                },
                bearerToken: IoC.ApplicationViewModel.ClientToken
                );
            
            // if the response has an error -> display it
            if (await result.DisplayErrorIfFailedAsync("Failed to add Password")) {
                // done
                return;
            }

            // add the new Account to the view
            Accounts.Add(new PasswordListItemViewModel {
                Id = result.ServerResponse.Response.Id,
                AccountName = viewModel.AccountName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Username = viewModel.Username,
                Website = viewModel.Website,
                Notes = viewModel.Notes,
            }) ;
        }


        #endregion

    }
}
