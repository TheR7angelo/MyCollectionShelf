using MyCollectionShelf.Maui.Ui.Desktop;
using MyCollectionShelf.Maui.Ui.SmartPhone;

namespace MyCollectionShelf.Maui
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            
            #if IOS || ANDROID
                MainPage = new NavigationPage(new SmartPhoneStartPage());
            #else
                MainPage = new NavigationPage(new DesktopStartPage());
            #endif
        }
    }
}