using System.Threading.Tasks;

namespace PasswordManager.Core {
    public interface IUIManager {

        /// <summary>
        /// Displays a dialog to add or edit a entry to the user content list
        /// </summary>
        /// <param name="viewModel">The view model to display</param>
        /// <returns></returns>
        Task ShowModifyDialog(DialogPasswordItemViewModel viewModel, string title);

    }
}
