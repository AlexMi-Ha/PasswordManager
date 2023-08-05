using PasswordManager.Core;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager {

    public class WindowViewModel : BaseViewModel {

        private Window window;

        private int _outerMarginSize = 10;

        private WindowDockPosition dockPosition = WindowDockPosition.Undocked;

        public double MinimumWindowHeight { get; set; } = 400;
        public double MinimumWindowWidth { get; set; } = 400;

        public bool Borderless { get { return (window.WindowState == WindowState.Maximized || dockPosition != WindowDockPosition.Undocked); } }

        public int ResizeBorder { get { return Borderless ? 0 : 6; } }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public int OuterMarginSize { 
            get {
                return window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
            } 
            set { _outerMarginSize = value; }
        }
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int TitleHeight { get; set; } = 25;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }



        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        public WindowViewModel(Window window) {
            this.window = window;

            // Listen for window resizing
            window.StateChanged += (sender, e) => {
                WindowResized();
            };
            window.Closed += (sender, e) => {
                window.Close();
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

        private void WindowResized() {
            //fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
        }


        private Point GetMousePosition() {
            var pos = Mouse.GetPosition(window);
            return new Point(pos.X + window.Left, pos.Y + window.Top);
        }
    }
}
