using System;
using System.Windows;
using MyCollectionShelf.Sql.Object.Book.Class.View;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;

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

    private void ToolTip_OnOpened(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(VBookShelf.SeriesSummarize);
    }
}