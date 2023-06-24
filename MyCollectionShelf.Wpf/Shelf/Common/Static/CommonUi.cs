﻿using System.Windows;
using System.Windows.Media;

namespace MyCollectionShelf.Wpf.Shelf.Common.Static;

public static class CommonUi
{
    public static T? FindParent<T>(this DependencyObject child) where T : DependencyObject
    {
        var parent = VisualTreeHelper.GetParent(child);
    
        while (parent != null && parent is not T)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
    
        return parent as T;
    }
}