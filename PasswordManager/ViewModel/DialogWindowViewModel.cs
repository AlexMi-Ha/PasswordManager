using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {

    /// <summary>
    /// ViewModel for the main Window
    /// </summary>
    public class DialogWindowViewModel : WindowViewModel {

        #region Private Attributes
        private Window window;
        #endregion


        #region Public Properties

        /// <summary>
        /// Title of the dialog window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content to host inside the dialog
        /// </summary>
        public Control Content { get; set; }

        /// <summary>
        /// The thickness of the resize border of the custom chrome
        /// </summary>
        public bool Resizeable {
            get => window.ResizeMode != ResizeMode.NoResize;
            set {
                window.ResizeMode = value ? ResizeMode.CanResize : ResizeMode.NoResize;
            }
        }
        public new int ResizeBorder { get => Resizeable ? 6 : 0; }
        public new Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder);  }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogWindowViewModel(Window window) : base(window) {
            MinimumWindowHeight = 100;
            MinimumWindowWidth = 250;
            this.window = window;
        }
        #endregion

    }
}
