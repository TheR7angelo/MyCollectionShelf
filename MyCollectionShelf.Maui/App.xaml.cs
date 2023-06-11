using MyCollectionShelf.Maui.Ui.SmartPhone;
using MyCollectionShelf.Maui.Ui.Desktop;

namespace MyCollectionShelf.Maui
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            
            #if IOS || ANDROID
                MainPage = new SmartPhoneAppShell();
            #else
                MainPage = new DesktopAppShell();
            #endif
        }
    }
}