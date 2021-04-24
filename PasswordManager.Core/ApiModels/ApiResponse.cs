namespace PasswordManager.Core {
    public class ApiResponse {

        #region Public Properties

        /// <summary>
        /// Indicates if the api call was successfull
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// error message for a failed Api call
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion
    }

    
    public class ApiResponse<T> : ApiResponse {
        public T Response { get; set; }
    }
}
