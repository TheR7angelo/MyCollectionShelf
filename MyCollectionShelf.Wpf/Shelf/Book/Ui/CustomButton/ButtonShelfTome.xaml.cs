using System.Windows;
using MyCollectionShelf.Sql.Table.Book;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;

public partial class ButtonShelfTome
{
    public static readonly DependencyProperty BookInformationsProperty =
        DependencyProperty.Register(nameof(BookInformations), typeof(BookInformations), typeof(ButtonShelfTome),
            new PropertyMetadata(default(BookInformations)));

    public ButtonShelfTome()
    {
        InitializeComponent();
    }

    public BookInformations BookInformations
    {
        get => (BookInformations)GetValue(BookInformationsProperty);
        set => SetValue(BookInformationsProperty, value);
    }
}