using System.Windows;

namespace MyCollectionShelf.Ui.Book.Pages;

public partial class AddEditBook
{
    public static readonly DependencyProperty BookTitleProperty = DependencyProperty.Register(nameof(BookTitle), typeof(string), typeof(AddEditBook), new PropertyMetadata(default(string)));

    public AddEditBook()
    {
        InitializeComponent();
    }

    public string BookTitle
    {
        get => (string)GetValue(BookTitleProperty);
        set => SetValue(BookTitleProperty, value);
    }
}