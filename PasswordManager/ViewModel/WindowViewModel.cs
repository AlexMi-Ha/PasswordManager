using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager {

    /// <summary>
    /// ViewModel for the main Window
    /// </summary>
    class WindowViewModel : BaseViewModel {

        #region Private Attributes
        /// <summary>
        /// The window this Viewmodel controls
        /// </summary>
        private Window window;

        /// <summary>
        /// Margin around the window for the Drop Shadow
        /// </summary>
        private int _outerMarginSize = 10;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition dockPosition = WindowDockPosition.Undocked;
        #endregion

        #region Public Properties

        

        /// <summary>
        /// Minimum Size the window can be resized to
        /// </summary>
        public double MinimumWindowHeight { get; set; } = 400;
        public double MinimumWindowWidth { get; set; } = 400;

        public bool Borderless { get { return (window.WindowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked); } }

        /// <summary>
        /// The thickness of the resize border of the custom chrome
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        /// <summary>
        /// Margin around the window for the Drop Shadow
        /// </summary>
        public int OuterMarginSize { 
            get {
                return window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
            } 
            set { _outerMarginSize = value; }
        }
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// Height of the Titlebar of the Window
        /// </summary>
        public int TitleHeight { get; set; } = 25;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        /// <summary>
        /// The current Page of the Application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;

        #endregion

        #region Commands
        /// <summary>
        /// Command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Command to show the system menu when clicking the icon
        /// </summary>
        public ICommand MenuCommand { get; set; }
        #endregion

        #region Constructor
        public WindowViewModel(Window window) {
            this.window = window;

            // Listen for window resizing
            window.StateChanged += (sender, e) => {
                WindowResized();
            };
            window.Closed += (sender, e) => {
                Environment.Exit(0);
            };

            // Create Commands
            MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
            CloseCommand = new RelayCommand(() => window.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(window, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(window);

            // Listen for dock changes
            resizer.WindowDockChanged += (dock) => {
                dockPosition = dock;
                // Call resize events
                WindowResized();
            };
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// EventHandler when the Window is resized
        /// </summary>
        private void WindowResized() {
            //fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
        }


        /// <summary>
        /// Gets the current mousePos on the Screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition() {
            var pos = Mouse.GetPosition(window);
            return new Point(pos.X + window.Left, pos.Y + window.Top);
        }
        #endregion
    }
}
