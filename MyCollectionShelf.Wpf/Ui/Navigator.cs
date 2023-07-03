using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyCollectionShelf.Wpf.Ui;

public static class Navigator
{
    private static NavigationService NavigationService { get; } =
        (Application.Current.MainWindow as MainWindow)!.MainFrame.NavigationService;

    public static void Navigate(string path, object? param = null)
        => NavigationService.Navigate(new Uri(path, UriKind.RelativeOrAbsolute), param);
    
    public static void Navigate(this Page page)
        => NavigationService.Navigate(page);

    public static void GoBack()
        => NavigationService.GoBack();

    public static void GoForward()
        => NavigationService.GoForward();
}