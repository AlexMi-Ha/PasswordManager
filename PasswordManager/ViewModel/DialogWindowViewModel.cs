using System.Windows;
using System.Windows.Controls;

namespace PasswordManager {

    public class DialogWindowViewModel : WindowViewModel {

        private Window window;


        public string Title { get; set; }

        public Control Content { get; set; }

        public bool Resizeable {
            get => window.ResizeMode != ResizeMode.NoResize;
            set {
                window.ResizeMode = value ? ResizeMode.CanResize : ResizeMode.NoResize;
            }
        }
        public new int ResizeBorder { get => Resizeable ? 6 : 0; }
        public new Thickness ResizeBorderThickness { get => new Thickness(ResizeBorder);  }

        public DialogWindowViewModel(Window window) : base(window) {
            MinimumWindowHeight = 100;
            MinimumWindowWidth = 250;
            this.window = window;
        }

    }
}
