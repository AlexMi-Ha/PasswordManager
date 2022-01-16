
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
        /// Email of the user trying to log in
        /// </summary>
        public string Email { get; set; }

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
            LoginCommand = new RelayParameterizedCommand<IHavePassword>(async (parameter) => await LoginAsync(parameter));

        }
        #endregion


        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>SecureString passed in from the view</returns>
        public async Task LoginAsync(IHavePassword parameter) {

            await RunCommandAsync(() => this.LoginIsRunning, async () => {

                // Call the database
                var result = await IoC.ClientDataStore.CheckLoginAsync(new LoginCredentialsDataModel
                {
                    Email = Email,
                    Password = parameter.SecurePassword.Unsecure(),
                });

                // if the response has an error -> display it
                if(result == null) {
                    // done
                    await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Login Failed" }, "Login failed!");
                    return;
                }
                // if we got here -> successfully logged in

                IoC.ApplicationViewModel.MasterHash = Crypt.Hash(parameter.SecurePassword.Unsecure());

                // let the application view model what happens on the successful login
                IoC.ApplicationViewModel.HandleSuccessfulLogin(result);
            });

        }
    }
}
