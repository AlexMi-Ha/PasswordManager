using Dna;
using System;
using System.Collections.ObjectModel;
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

            Accounts = new ObservableCollection<PasswordListItemViewModel>();

            Accounts.Add(new PasswordListItemViewModel() { AccountName ="Bros", Email="bros@Bro.com", Password="1"});
            Accounts.Add(new PasswordListItemViewModel() { AccountName="JuttaEureLehrerin", Email="jutta.EureLehrerin@gmail.com", Password = "2" });
            Accounts.Add(new PasswordListItemViewModel() { AccountName="foo3", Email="foo@bar3.de", Password = "3" });
            Accounts.Add(new PasswordListItemViewModel() { AccountName = "foo4", Email = "foo@bar4.tk", Password = "4" });
            Accounts.Add(new PasswordListItemViewModel() { AccountName = "foo5", Email = "foo@bar5.io", Password = "5" });
            Accounts.Add(new PasswordListItemViewModel() { AccountName= "Daniel", Email = "Daniel@Stinkt.bah", Password = "6" });
        }
        #endregion

        #region Methods

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
            DialogPasswordItemViewModel viewModel = new DialogPasswordItemViewModel { 
                Notes = "TODO"
            };
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
                    // TODO encrypt info and send to server       
                }) ;
            
            // if the response has an error -> display it
            if (await result.DisplayErrorIfFailedAsync("Failed to add Password")) {
                // done
                return;
            }

        }


        #endregion

    }
}
