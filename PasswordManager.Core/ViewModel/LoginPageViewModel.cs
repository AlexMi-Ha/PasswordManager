
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordManager.Core {
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

                await Task.Delay(1000);
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });

        }
    }
}
