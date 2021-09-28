
namespace PasswordManager.Data.DataModels {
    public class LoginDatabaseModel {

        /// <summary>
        /// Unique id of this user
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The users password
        /// </summary>
        public string Password { get; set; }
    }
}
