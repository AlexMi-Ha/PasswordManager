using System.Threading.Tasks;

namespace PasswordManager.Core.Common.Interfaces.Services {
    public interface IUIManager {

        /// <summary>
        /// Displays a dialog to add or edit a entry to the user content list
        /// </summary>
        /// <param name="viewModel">The view model to display</param>
        /// <param name="title">title of the modify dialog</param>
        /// <returns></returns>
        Task ShowModifyDialog(DialogPasswordItemViewModel viewModel, string title);

        /// <summary>
        /// Displays a dialog that displays a message
        /// </summary>
        /// <param name="viewModel">view model to display</param>
        /// <param name="title">title of the message box dialog</param>
        /// <returns></returns>
        Task ShowMessageBoxDialog(DialogMessageBoxViewModel viewModel, string title);

        /// <summary>
        /// Displays a dialg that displays a message and gives an ok and cancel button
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task ShowChoiceBoxDialog(DialogChoiceBoxViewModel viewModel, string title);

        /// <summary>
        /// Display a Search dialog box
        /// </summary>
        /// <param name="viewModel">ViewModel to display</param>
        /// <returns></returns>
        Task SearchDialog(DialogSearchBoxViewModel viewModel, string title);
    }
}
