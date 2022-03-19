using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManager.Core {
    public class RegisterResultDataModel {
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

        public RegisterErrorType ErrorType { get; set; } = RegisterErrorType.Error;

        #endregion

    }

    public enum RegisterErrorType { 
        None = 0,
        UserAlreadyExists = 1,
        Error = 2,
    }
}
