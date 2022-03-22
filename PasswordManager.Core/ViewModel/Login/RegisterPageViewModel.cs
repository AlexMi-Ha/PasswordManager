
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
    public class RegisterPageViewModel : BaseViewModel {


        #region Public Properties

        public string Email { get; set; }

        public bool RegisterIsRunning { get; set; }

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; set; }

        public ICommand ChangeToLoginCommand { get; set; }

        #endregion

        public RegisterPageViewModel() {
            RegisterCommand = new RelayParameterizedCommand<IHavePassword>(async (param) => await RegisterAsync(param));

            ChangeToLoginCommand = new RelayCommand(() => {
                IoC.ApplicationViewModel.GoToPage(ApplicationPage.Login);
            });
            }

        public async Task RegisterAsync(IHavePassword parameter) {

            await RunCommandAsync(() => this.RegisterIsRunning, async () => {

                if (parameter.SecurePassword.Count != 2 || !parameter.SecurePassword[0].Unsecure().Equals(parameter.SecurePassword[1].Unsecure())) {
                    await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Passwords do not match!" }, "Register failed!");
                    return;
                }

                // Call the database
                RegisterResultDataModel result = await IoC.ClientDataStore.AddNewUserAsync(new LoginCredentialsDataModel {
                    Email = Email,
                    Password = parameter.SecurePassword[0].Unsecure(),
                });

                // if the response has an error -> display it
                if (result == null || ( !result.Successful && result.ErrorType == RegisterErrorType.Error )) {
                    // done
                    await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Register Failed" }, "Register failed!");
                    return;
                }

                if (!result.Successful && result.ErrorType == RegisterErrorType.UserAlreadyExists) {
                    // done
                    await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = "Register Failed: User already exists!" }, "Register failed!");
                    return;
                }


                // if we got here -> successfully logged in

                IoC.ApplicationViewModel.MasterHash = Crypt.Hash(parameter.SecurePassword[0].Unsecure());

                // let the application view model what happens on the successful login
                IoC.ApplicationViewModel.HandleSuccessfulLogin(new LoginResultDataModel { 
                    Email = result.Email,
                    Successful = result.Successful,
                    UserId = result.UserId
                }) ;
            });

        }

    }
}
