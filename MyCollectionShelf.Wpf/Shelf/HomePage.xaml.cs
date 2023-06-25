using System.Windows;

namespace MyCollectionShelf.Wpf.Shelf;

public partial class HomePage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var bookPage = new Book.Ui.Pages.BookShelf();
        bookPage.Navigate();
    }
}