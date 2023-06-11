namespace MyCollectionShelf.Maui.Ui.Desktop
{
    public partial class DesktopAppShell
    {
        public DesktopAppShell()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object? sender, EventArgs e)
        {
            await Current.GoToAsync(new DesktopStartPage().Title);
        }
    }
}