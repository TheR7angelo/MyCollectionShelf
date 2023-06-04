using System;
using System.Windows;

namespace MyCollectionShelf.Ui.Book.UserControls;

public partial class BookInformation
{
    public static readonly DependencyProperty BookInformationDataProperty = DependencyProperty.Register(nameof(BookInformationData), typeof(MyCollectionShelf.Book.Object.Class.BookInformations), typeof(BookInformation), new PropertyMetadata(default(MyCollectionShelf.Book.Object.Class.BookInformations)));

    public BookInformation()
    {
        InitializeComponent();
    }

    public MyCollectionShelf.Book.Object.Class.BookInformations BookInformationData
    {
        get => (MyCollectionShelf.Book.Object.Class.BookInformations)GetValue(BookInformationDataProperty);
        set => SetValue(BookInformationDataProperty, value);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        BookInformationData.Authors.Add(new MyCollectionShelf.Book.Object.Class.BookAuthors());
        Console.WriteLine(BookInformationData.Authors.Count);
    }
}