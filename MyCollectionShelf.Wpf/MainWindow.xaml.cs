using System.Globalization;
using System.Threading;
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

            var cultureFr = new CultureInfo("fr");
            var cultureEn = new CultureInfo("en");

            Thread.CurrentThread.CurrentCulture = cultureEn;
            Thread.CurrentThread.CurrentUICulture = cultureEn;
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