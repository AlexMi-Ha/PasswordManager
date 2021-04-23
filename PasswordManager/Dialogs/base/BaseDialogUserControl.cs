using PasswordManager.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {
    /// <summary>
    /// The base class for any content that is being used inside of a DialogWindow
    /// </summary>
    public class BaseDialogUserControl : UserControl, ICloseable {

        #region Private Attributes
        /// <summary>
        ///  The dialog window this will be contained in
        /// </summary>
        private DialogWindow dialogWindow;
        #endregion


        #region Public Commands
        #endregion

        #region Public Properties

        /// <summary>
        /// MinimumWidth of this dialog
        /// </summary>
        public int MinimumWindowWidth { get; set; } = 250;

        /// <summary>
        /// Minimum height of this dialog
        /// </summary>
        public int MinimumWindowHeight { get; set; } = 100;

        /// <summary>
        /// Title of this dialog
        /// </summary>
        public string Title { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDialogUserControl(bool resizeable = false) {
            Name = "DialogUserControl";
            // create a new dialogwindow
            dialogWindow = new DialogWindow();
            dialogWindow.ViewModel = new DialogWindowViewModel(dialogWindow);
            if (resizeable) dialogWindow.ViewModel.Resizeable = true;
        }
        #endregion

        #region Public Dialog Methods

        /// <summary>
        /// Displays a dialog
        /// </summary>
        /// <param name="viewModel">The View Model to display</param>
        /// <typeparam name="T">The view model type for this control</typeparam>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel) where T : BaseViewModel {
            // Create a task to await the dialog closing
            var tcs = new TaskCompletionSource<bool>();
            // first get off of the UI thread to avoid a deadlock when awaiting the UI thread
            Task.Run(() => {
                // Run on UI thread
                Application.Current.Dispatcher.Invoke(() => {
                    try {
                        // Match controls expected sizes to the dialogs view model
                        dialogWindow.ViewModel.MinimumWindowHeight = MinimumWindowHeight;
                        dialogWindow.ViewModel.MinimumWindowWidth = MinimumWindowWidth;
                        dialogWindow.ViewModel.Title = Title;

                        //set this control to the dialog window content
                        dialogWindow.ViewModel.Content = this;

                        // set up this controls data context binding to the view model
                        DataContext = viewModel;


                        // Show the dialog
                        dialogWindow.ShowDialog();

                    } finally {
                        // let the caller know the task finished
                        tcs.TrySetResult(true);
                    }
                });
            });

            return tcs.Task;
        }


        public void Close() {
            dialogWindow.Close();
        }

        #endregion

        }
}
