using System.Windows;
using MyCollectionShelf.Sql.View.Book;

namespace MyCollectionShelf.Wpf.Ui.Book.Ui.CustomButton;

public partial class ButtonShelfBook
{
    public static readonly DependencyProperty VBookShelfProperty = DependencyProperty.Register(nameof(VBookShelf),
        typeof(VBookShelf), typeof(ButtonShelfBook), new PropertyMetadata(default(VBookShelf)));

    public string Owned => $"{ButtonShelfBookResources.Owned}: {VBookShelf.Total}";
    public string Read => $"{ButtonShelfBookResources.Read}: {VBookShelf.Read}";
    public string NotRead => $"{ButtonShelfBookResources.NotRead}: {VBookShelf.NotRead}";
    public string AveragePage => $"{ButtonShelfBookResources.AveragePage}: {VBookShelf.AveragePage}";
    
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