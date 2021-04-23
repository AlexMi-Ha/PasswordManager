namespace PasswordManager.Core {
    /// <summary>
    /// Interface used to copy something to clipboard
    /// </summary>
    public interface ICopyClipboard {
        /// <summary>
        /// Copy the text to the clipboard
        /// </summary>
        /// <param name="text">Text to get copied</param>
        public void CopyToClipboard(string text);
    }
}
