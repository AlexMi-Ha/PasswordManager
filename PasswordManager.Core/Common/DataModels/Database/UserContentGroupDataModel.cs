
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Core.Common.DataModels.Database
{
    public class UserContentGroupDataModel
    {

        public string Id { get; set; }

        public string GroupName { get; set; }

        public List<UserContentDataModel> Content { get; set; }

        public UserDataModel User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
