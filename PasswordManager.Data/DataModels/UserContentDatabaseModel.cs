using PasswordManager.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Data.DataModels {
    public class UserContentDatabaseModel : UserContentDataModel {

        // See UserContent Data Model for all properties


        /// <summary>
        /// Foreign key -> User Id this entry belongs to
        /// </summary>
        public LoginDatabaseModel User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
