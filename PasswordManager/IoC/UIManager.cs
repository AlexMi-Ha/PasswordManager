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
            PasswordItemDialogBox b = new PasswordItemDialogBox {
                Title = title
            };
            return b.ShowDialog(viewModel);
        }

        /// <summary>
        /// Display a Message in a dialog box
        /// </summary>
        /// <param name="viewModel">Viewmodel to display</param>
        /// <returns></returns>
        public Task ShowMessageBoxDialog(DialogMessageBoxViewModel viewModel, string title) {
            MessageDialogBox b = new MessageDialogBox {
                Title = title
            };
            return b.ShowDialog(viewModel);
        }

        /// <summary>
        /// Display a Search dialog box
        /// </summary>
        /// <param name="viewModel">ViewModel to display</param>
        /// <returns></returns>
        public Task SearchDialog(DialogSearchBoxViewModel viewModel, string title) {
            SearchDialogBox b = new SearchDialogBox {
                Title = title
            };
            return b.ShowDialog(viewModel);
        }

        public Task ShowChoiceBoxDialog(DialogChoiceBoxViewModel viewModel, string title) {
            ChoiceDialogBox b = new ChoiceDialogBox {
                Title = title
            };
            return b.ShowDialog(viewModel);
        }
    }
}
