namespace PasswordManager.Core {
    /// <summary>
    /// The result of a login request 
    /// </summary>
    public class LoginResultDataModel {

        #region Public Properties

        /// <summary>
        /// Id of this User
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Was the request successful?
        /// </summary>
        public bool Successful { get; set; } = false;

        #endregion
    }
}
