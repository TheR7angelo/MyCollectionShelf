using MyCollection.Maui.Ui.Pages.Smartphone;
using MyCollection.Maui.Ui.Pages.Desktop;

namespace MyCollection.Maui
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