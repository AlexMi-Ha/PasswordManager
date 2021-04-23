namespace PasswordManager {
    /// <summary>
    /// Interaction logic for PasswordItemDialogMessageBox.xaml
    /// </summary>
    public partial class PasswordItemDialogBox : BaseDialogUserControl {
        public PasswordItemDialogBox() : base(resizeable: true) {
            InitializeComponent();
            MinimumWindowWidth = 750;
            MinimumWindowHeight = 400;
        }
    }
}
