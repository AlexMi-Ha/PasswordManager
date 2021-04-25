using Dna;
using System.Threading.Tasks;

namespace PasswordManager.Core {
    public static class WebRequestsResultExtensions {

        /// <summary>
        /// Checks the web requests response for error and displays them
        /// </summary>
        /// <param name="response">Response to check</param>
        /// <param name="title">Title of the error dialog if there was any</param>
        /// <returns>bool indicating if there was an error</returns>
        public static async Task<bool> DisplayErrorIfFailedAsync<T>(this WebRequestResult<ApiResponse<T>> response, string title) {
            // if there was no response, bad data or an error response  ->  error
            if (response == null || response.ServerResponse == null || !response.ServerResponse.Successful) {
                // Default error message
                // Todo localize strings
                var message = "Unknown error from server response";

                // Set message to server response
                if (response?.ServerResponse != null)
                    message = response.ServerResponse.ErrorMessage;
                // if we have a response but deserialization failed
                else if (!string.IsNullOrWhiteSpace(response?.RawServerResponse)) {
                    // set error message
                    message = $"Unexpected response from server. {response.RawServerResponse}";
                }
                // if we have a response but no response details
                else if (response != null) {
                    // Set message to standard http server response details
                    message = $"Failed to communicate to server. Status code {response.StatusCode}. {response.StatusDescription}";
                }

                // Display error
                await IoC.UI.ShowMessageBoxDialog(new DialogMessageBoxViewModel { Message = message }, title);

                // return that we hat an error
                return true;
            }
            //all good
            return false;
        }
    }
}
