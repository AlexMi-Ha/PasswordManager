namespace PasswordManager.Core {

    /// <summary>
    /// Data model to represent one User Content entry
    /// </summary>
    public class UserContentDataModel {

        #region Public Properties

        /// <summary>
        /// Id of this account
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Hash of the accountname
        /// </summary>
        public string AccountNameHash { get; set; }
        /// <summary>
        /// hash of the email
        /// </summary>
        public string EmailHash { get; set; }
        /// <summary>
        /// hash of the username
        /// </summary>
        public string UsernameHash { get; set; }
        /// <summary>
        /// hash of the website
        /// </summary>
        public string WebsiteHash { get; set; }
        /// <summary>
        /// hash of the password
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// hash of the notes
        /// </summary>
        public string NotesHash { get; set; }

        #endregion

    }
}
