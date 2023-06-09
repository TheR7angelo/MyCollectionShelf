namespace MyCollection.Maui;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Button2_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FullScreenPage());
    }
}