using System.Windows;

namespace MyCollectionShelf.Ui.Book.UserControls;

public partial class AddEditAuthors
{
    public static readonly DependencyProperty BoxTitleProperty = DependencyProperty.Register(nameof(BoxTitle), typeof(string), typeof(AddEditAuthors), new PropertyMetadata(default(string)));
    public static readonly DependencyProperty BoxTextProperty = DependencyProperty.Register(nameof(BoxText), typeof(string), typeof(AddEditAuthors), new PropertyMetadata(default(string)));

    public AddEditAuthors()
    {
        InitializeComponent();
    }

    public string BoxTitle
    {
        get => (string)GetValue(BoxTitleProperty);
        set => SetValue(BoxTitleProperty, value);
    }

    public string BoxText
    {
        get => (string)GetValue(BoxTextProperty);
        set => SetValue(BoxTextProperty, value);
    }
}