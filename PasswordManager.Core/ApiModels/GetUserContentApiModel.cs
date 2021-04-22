using System.Collections.Generic;

namespace PasswordManager.Core {
    /// <summary>
    /// Api Model to retrieve the users content
    /// </summary>
    public class GetUserContentApiModel {

        #region Public Properties
        public List<UserContentApiModel> UserContent { get; set; }
        #endregion

    }

    
}
