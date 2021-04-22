namespace PasswordManager.Core {
    /// <summary>
    /// Credentials for an API client to log into the server
    /// </summary>
    public class LoginCredentialsApiModel {

        #region Public Properties
        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The users password
        /// </summary>
        public string Password { get; set; }
        #endregion

        

    }
}
