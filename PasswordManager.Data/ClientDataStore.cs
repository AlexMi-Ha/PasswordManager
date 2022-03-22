using PasswordManager.Core;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.DataModels;

namespace PasswordManager.Data {
    public class ClientDataStore : IClientDataStore {

        #region Protected Members

        protected ClientDataStoreDbContext dbContext;

        #endregion

        #region Constructor

        public ClientDataStore(ClientDataStoreDbContext dbContext) {
            this.dbContext = dbContext;
        }

        #endregion

        #region Interface Implementation

        public async Task EnsureDataStoreAsync() {
            await dbContext.Database.EnsureCreatedAsync();
        }

        public async Task<LoginResultDataModel> CheckLoginAsync(LoginCredentialsDataModel loginCredentials) {
            if (string.IsNullOrWhiteSpace(loginCredentials.Email)) {
                return new LoginResultDataModel { Successful = false };
            }

            var user = dbContext.LoginCredentials.Where(e => e.Email == loginCredentials.Email && e.Password == HashString(loginCredentials.Password)).FirstOrDefault();
            
            if(user == null) {
                return new LoginResultDataModel {
                    Successful = false
                };
            }

            return new LoginResultDataModel {
                Email = user.Email,
                Successful = true,
                UserId = user.UserId
            };

        }

        public async Task<RegisterResultDataModel> AddNewUserAsync(LoginCredentialsDataModel loginCredentials) {
            if (string.IsNullOrWhiteSpace(loginCredentials.Email)) {
                return new RegisterResultDataModel { Successful = false, ErrorType = RegisterErrorType.Error};
            }


            var users = dbContext.LoginCredentials.Where(e => e.Email == loginCredentials.Email);

            if(users?.Count() > 0) {
                return new RegisterResultDataModel { Successful = false, ErrorType = RegisterErrorType.UserAlreadyExists };
            }
            //var user = users.Where(e => e.Email == loginCredentials.Email && e.Password == HashString(loginCredentials.Password)).FirstOrDefault();

            string userId = Guid.NewGuid().ToString("N");
            bool unique;
            do {
                unique = true;
                if (dbContext.LoginCredentials.Where(e => e.UserId == userId).Any()) {
                    userId = Guid.NewGuid().ToString("N");
                    unique = false;
                }
            } while (!unique);
            dbContext.LoginCredentials.Add(new LoginDatabaseModel {
                Email = loginCredentials.Email,
                Password = HashString(loginCredentials.Password),
                UserId = userId
            });

            await dbContext.SaveChangesAsync();

            return new RegisterResultDataModel { Successful = true, ErrorType = RegisterErrorType.None, Email = loginCredentials.Email, UserId=userId };
        }

        public async Task<GetUserContentDataModel> GetUserContentAsync(LoginResultDataModel loginInfo) {

            if (!LoginInformationComplete(loginInfo))
                return null;

            return new GetUserContentDataModel {
                UserContent = await dbContext.UserContent
                    .Where(e => e.User.UserId == loginInfo.UserId)
                    .Select(c => new UserContentDataModel {
                        Id = c.Id,
                        AccountNameHash = c.AccountNameHash,
                        EmailHash = c.EmailHash,
                        UsernameHash = c.UsernameHash,
                        WebsiteHash = c.WebsiteHash,
                        PasswordHash = c.PasswordHash,
                        NotesHash = c.NotesHash
                    }).ToListAsync()
            };
        }

        public async Task<UserContentDataModel> UpdateUserContentAsync(LoginResultDataModel loginInfo, UserContentDataModel model) {
            if (!LoginInformationComplete(loginInfo))
                return null;

            var table = await dbContext.UserContent.Where(e => e.User.UserId == loginInfo.UserId).Where(e => e.Id == model.Id).ToListAsync();

            if (table == null || table.Count != 1)
                return null;

            table.First().AccountNameHash = model.AccountNameHash;
            table.First().EmailHash = model.EmailHash;
            table.First().UsernameHash = model.UsernameHash;
            table.First().WebsiteHash = model.WebsiteHash;
            table.First().PasswordHash = model.PasswordHash;
            table.First().NotesHash = model.NotesHash;

            await dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<UserContentDataModel> AddUserContentAsync(LoginResultDataModel loginInfo, UserContentDataModel model) {

            if (!LoginInformationComplete(loginInfo))
                return null;

            bool unique;
            do {
                unique = true;
                if (dbContext.UserContent.Where(e => e.User.UserId == loginInfo.UserId).Where(e => e.Id == model.Id).Any()) {
                    model.Id = Guid.NewGuid().ToString("N");
                    unique = false;
                }
            } while (!unique);

            dbContext.UserContent.Add(new UserContentDatabaseModel {
                Id = model.Id,
                User = dbContext.LoginCredentials.Where(e => e.UserId == loginInfo.UserId).First(),
                AccountNameHash = model.AccountNameHash,
                EmailHash = model.EmailHash,
                UsernameHash = model.UsernameHash,
                WebsiteHash = model.WebsiteHash,
                PasswordHash = model.PasswordHash,
                NotesHash = model.NotesHash,
            });

            await dbContext.SaveChangesAsync();

            return model;

        }

        public async Task<bool> RemoveUserContentAsync(LoginResultDataModel loginInfo, string IDToRemove) {

            if (!LoginInformationComplete(loginInfo))
                return false;

            var table = await dbContext.UserContent.Where(e => e.Id == IDToRemove).Where(e => e.User.UserId == loginInfo.UserId).ToListAsync();

            if (table == null || table.Count != 1)
                return false;

            dbContext.UserContent.Remove(table.First());

            await dbContext.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Helpers

        private bool LoginInformationComplete(LoginResultDataModel loginInfo) {
            return !(!loginInfo.Successful ||  string.IsNullOrWhiteSpace(loginInfo.UserId));
        }

        private string HashString(string s) {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create()) {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(s));

                foreach (byte b in result) {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
