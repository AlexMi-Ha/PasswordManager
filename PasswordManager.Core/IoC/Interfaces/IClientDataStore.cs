
using System.Threading.Tasks;

namespace PasswordManager.Core {
    public interface IClientDataStore {

        /// <summary>
        /// Makes sure the client data store is correctly set up
        /// </summary>
        /// <returns></returns>
        Task EnsureDataStoreAsync();

        /// <summary>
        /// Check if the given login credentials are right and then login
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        Task<LoginResultDataModel> CheckLoginAsync(LoginCredentialsDataModel loginCredentials);

        /// <summary>
        /// Add a new user to the login database
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        Task<RegisterResultDataModel> AddNewUserAsync(LoginCredentialsDataModel loginCredentials);

        /// <summary>
        /// Get all user content from a specific user
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        Task<GetUserContentDataModel> GetUserContentAsync(LoginResultDataModel loginInfo);

        /// <summary>
        /// Update one user content entry of a specific user
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserContentDataModel> UpdateUserContentAsync(LoginResultDataModel loginInfo, UserContentDataModel model);

        /// <summary>
        /// Add one user content entry to a specific user
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserContentDataModel> AddUserContentAsync(LoginResultDataModel loginInfo, UserContentDataModel model);

        /// <summary>
        /// Remove one user content entry of a specific user
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="IDToRemove"></param>
        /// <returns></returns>
        Task<bool> RemoveUserContentAsync(LoginResultDataModel loginInfo, string IDToRemove);



    }
}
