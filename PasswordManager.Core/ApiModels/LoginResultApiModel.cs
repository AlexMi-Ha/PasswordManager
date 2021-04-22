namespace PasswordManager.Core {
    /// <summary>
    /// The result of a login request via the api
    /// </summary>
    public class LoginResultApiModel {

        #region Public Properties

        /// <summary>
        /// The authentication Token to stay authenticated through future requests
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }

        #endregion
    }
}
