using System;
using System.Windows;
using MyCollectionShelf.Wpf.Object.Class.Enum;

namespace MyCollectionShelf.Wpf.Ui.General.ButtonCustom;

public partial class ButtonAddRemove
{
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode),
        typeof(EAddRemove), typeof(ButtonAddRemove), new PropertyMetadata(default(EAddRemove), PropertyMode_Changed));

    public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(nameof(ImageUri),
        typeof(Uri), typeof(ButtonAddRemove), new PropertyMetadata(default(Uri)));
    
    private static readonly Uri PlusUri = new("/Resources/Assets/plus.svg", UriKind.Relative);
    private static readonly Uri MinusUri = new("/Resources/Assets/minus.svg", UriKind.Relative);

    private static void PropertyMode_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sender = (ButtonAddRemove)d;
        sender.Mode = (EAddRemove)e.NewValue;

        sender.ImageUri = sender.Mode switch
        {
            EAddRemove.Add => PlusUri,
            EAddRemove.Remove => MinusUri,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ButtonAddRemove()
    {
        ImageUri = PlusUri;
        InitializeComponent();
    }

    public EAddRemove Mode
    {
        get => (EAddRemove)GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    public Uri ImageUri
    {
        get => (Uri)GetValue(ImageUriProperty);
        set => SetValue(ImageUriProperty, value);
    }
}