using System.Windows;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.UserControls;

public partial class UserControl1
{
    public static readonly DependencyProperty CoverPathProperty = DependencyProperty.Register(nameof(CoverPath),
        typeof(string), typeof(UserControl1), new PropertyMetadata(default(string)));

    public UserControl1()
    {
        CoverPath = @"C:\Users\ZP6177\Creative Cloud Files\Programmation\C#\Ineo Infracom\MyCollectionShelf\MyCollectionShelf.Wpf\Resources\cover.jpg";
        InitializeComponent();
    }

    public string CoverPath
    {
        get => (string)GetValue(CoverPathProperty);
        set => SetValue(CoverPathProperty, value);
    }
}