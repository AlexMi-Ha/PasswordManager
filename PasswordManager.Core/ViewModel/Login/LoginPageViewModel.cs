﻿
using Dna;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
    /// <summary>
    /// View model for the login page
    /// </summary>
    public class LoginPageViewModel : BaseViewModel {


        #region Public Properties

        /// <summary>
        /// Flag indicating if the logincommand is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion


        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion

        #region Contructor
        public LoginPageViewModel() {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
        }
        #endregion


        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>SecureString passed in from the view</returns>
        public async Task LoginAsync(object parameter) {

            await RunCommandAsync(() => this.LoginIsRunning, async () => {

                // Call the server
                // Todo add email to login
                var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(
                    ApiRoutes.ServerAdress + ApiRoutes.Login,
                    new LoginCredentialsApiModel { 
                        Email = "foo@bar.de",
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure(),
                });

                // if the response has an error -> display it
                if(await result.DisplayErrorIfFailedAsync("Login Failed")) {
                    // done
                    return;
                }

                // if we got here -> successfully logged in

                // let the application view model what happens on the successful login
                IoC.Get<ApplicationViewModel>().HandleSuccessfulLogin(result.ServerResponse.Response);
            });

        }
    }
}