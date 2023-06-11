using MyCollectionShelf.Book;

namespace MyCollectionShelf.Maui.Ui.Desktop;
using CommunityToolkit.Maui.Views;

public partial class DesktopStartPage
{
    public DesktopStartPage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        // const string isbn = "9782380713084";
        //
        // var allApi = new BookAllApi();
        // var book = await allApi.GetBookInformation(isbn);
        //
        // await DisplayAlert("Titre", book.BookInformations.Title, "ok");

        var window = new Window();
        Application.Current?.OpenWindow(window);

    }
}