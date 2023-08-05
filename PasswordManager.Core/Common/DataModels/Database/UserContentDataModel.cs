using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Core.Common.DataModels.Database
{

    public class UserContentDataModel
    {

        public string Id { get; set; }
        
        public string AccountNameHash { get; set; }
        public string EmailHash { get; set; }
        public string UsernameHash { get; set; }
        public string WebsiteHash { get; set; }
        public string PasswordHash { get; set; }
        public string NotesHash { get; set; }


        public UserContentGroupDataModel ContentGroup { get; set; }

        [ForeignKey("ContentGroup")]
        public string ContentGroupId { get; set; }

    }
}
