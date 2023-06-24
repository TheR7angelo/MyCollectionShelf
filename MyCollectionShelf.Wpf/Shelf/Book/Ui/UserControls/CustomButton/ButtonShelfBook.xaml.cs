using System.Windows;
using MyCollectionShelf.Sql.Object.Book.Class.View;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.UserControls.CustomButton;

public partial class ButtonShelfBook
{
    public static readonly DependencyProperty VBookShelfProperty = DependencyProperty.Register(nameof(VBookShelf),
        typeof(VBookShelf), typeof(ButtonShelfBook), new PropertyMetadata(default(VBookShelf)));

    public ButtonShelfBook()
    {
        InitializeComponent();
    }

    public VBookShelf VBookShelf
    {
        get => (VBookShelf)GetValue(VBookShelfProperty);
        set => SetValue(VBookShelfProperty, value);
    }
}