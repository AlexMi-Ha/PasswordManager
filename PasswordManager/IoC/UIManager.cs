using PasswordManager.Core;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager {
    /// <summary>
    /// The application implementation of the IUIManager used to display system messages
    /// </summary>
    public class UIManager : IUIManager {
        /// <summary>
        /// Displays a dialog to add or edit an entry to the user content list
        /// </summary>
        /// <param name="viewModel">The View Model to display</param>
        /// <returns></returns>
        public Task ShowModifyDialog(DialogPasswordItemViewModel viewModel, string title) {
            PasswordItemDialogBox b = new PasswordItemDialogBox();
            b.Title = title;
            return b.ShowDialog(viewModel);
        }
    }
}
