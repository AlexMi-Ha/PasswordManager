
namespace PasswordManager {
    public class PasswordListItemDesignModel : PasswordListItemViewModel {

        #region Singleton
        public static PasswordListItemViewModel Instance => new PasswordListItemViewModel();
        #endregion

        #region Constructor
        public PasswordListItemDesignModel() {
            AccountName = "Account";
            Email = "account@Account.tk";
        }
        #endregion

    }
}
