﻿using System;
using System.Windows;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Controls.Buttons;

public partial class SvgButton
{
    public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(nameof(ImageUri),
        typeof(Uri), typeof(SvgButton), new PropertyMetadata(default(Uri)));

    public SvgButton()
    {
        InitializeComponent();
    }

    public Uri ImageUri
    {
        get => (Uri)GetValue(ImageUriProperty);
        set => SetValue(ImageUriProperty, value);
    }
}