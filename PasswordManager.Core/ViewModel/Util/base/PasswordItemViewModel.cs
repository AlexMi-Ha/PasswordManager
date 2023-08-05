
using PasswordManager.Core.ViewModel.Base;

namespace PasswordManager.Core.ViewModel.Util.Base {
    /// <summary>
    /// Viewmodel for a PasswordItem 
    /// </summary>
    public class PasswordItemViewModel : BaseViewModel {

        public string Id { get; set; }

        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }


        public PasswordItemViewModel() {

        }
    }
}
