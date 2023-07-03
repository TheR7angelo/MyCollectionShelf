using System.Windows;
using MaterialDesignThemes.Wpf;

namespace MyCollectionShelf.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            SetUpTheme();
        }

        private void SetUpTheme()
        {
            var palette = new PaletteHelper();
            var theme = palette.GetTheme();
            var baseTheme = theme.GetBaseTheme();

            if (baseTheme == BaseTheme.Dark)
            {
                ToggleButtonTheme.IsChecked = true;
            }
        }
        
        private void ToggleButtonTheme_OnChecked(object sender, RoutedEventArgs e)
        {
            var palette = new PaletteHelper();
            var theme = palette.GetTheme();

            theme.SetBaseTheme(ToggleButtonTheme.IsChecked!.Value ? Theme.Dark : Theme.Light);
            palette.SetTheme(theme);
        }
    }
}