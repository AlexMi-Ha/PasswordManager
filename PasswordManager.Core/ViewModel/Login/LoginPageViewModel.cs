
using PasswordManager.Core.Common.Interfaces.ViewModelAttributes;
using PasswordManager.Core.Security;
using PasswordManager.Core.ViewModel.Base;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PasswordManager.Core.ViewModel.Login {
    /// <summary>
    /// View model for the login page
    /// </summary>
    public class LoginPageViewModel : BaseViewModel {


        public string Email { get; set; }

        public bool LoginIsRunning { get; set; }


        public ICommand LoginCommand { get; set; }
        public ICommand ChangeFolderCommand { get; set; }

        public LoginPageViewModel() {
            LoginCommand = new RelayParameterizedCommand<IHavePassword>(async (parameter) => await LoginAsync(parameter));
            ChangeFolderCommand = new RelayCommand(ChangeFolder);
        }


        public void ChangeFolder() {
            using (var fbd = new FolderBrowserDialog()) {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    using (var f = new StreamWriter(".loc", false)) {
                        f.WriteLine(Crypt.EncryptString(Secret.Key,fbd.SelectedPath));
                        f.Flush();
                    }
                MessageBox.Show("Requires Restart!", "Restart", MessageBoxButtons.OK);
                Environment.Exit(0);
                }
            }
        }

        public async Task LoginAsync(IHavePassword parameter) {

            await RunCommandAsync(() => this.LoginIsRunning, async () => {

                // Call the database
                LoginResultDataModel result = await IoC.ClientDataStore.CheckLoginAsync(new LoginCredentialsDataModel
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
