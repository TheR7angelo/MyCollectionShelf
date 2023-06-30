using System;
using System.Windows;
using System.Windows.Controls;
using MyCollectionShelf.Wpf.Shelf.Common.Static;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog.MessageBox;

public partial class MessageDialog
{
    private string MessageContent { get; }
    
    private string Caption { get; } = string.Empty;

    private Uri? MessageBoxImage { get; }

    public MessageBoxResult MessageBoxResult { get; set; }
    
    public MessageDialog(string messageContent)
    {
        MessageContent = messageContent;
        InitializeComponent();
    }
    
    public MessageDialog(string messageContent, string caption)
    {
        MessageContent = messageContent;
        Caption = caption;
        InitializeComponent();
    }
    
    public MessageDialog(string messageContent, MessageBoxButton messageBoxButton)
    {
        MessageContent = messageContent;
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton);
    }
    
    public MessageDialog(string messageContent, string caption, MessageBoxButton messageBoxButton)
    {
        MessageContent = messageContent;
        Caption = caption;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton);
    }
    
    public MessageDialog(string messageContent, Uri messageBoxImage)
    {
        MessageContent = messageContent;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
    }
    
    public MessageDialog(string messageContent, string caption, Uri messageBoxImage)
    {
        MessageContent = messageContent;
        Caption = caption;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
    }
    
    public MessageDialog(string messageContent, MessageBoxButton messageBoxButton, Uri messageBoxImage)
    {
        MessageContent = messageContent;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton);
    }
    
    public MessageDialog(string messageContent, string caption, MessageBoxButton messageBoxButton, Uri messageBoxImage)
    {
        MessageContent = messageContent;
        Caption = caption;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton);
    }
    
    public MessageDialog(string messageContent, MessageBoxButton messageBoxButton, MessageBoxResult messageBoxResult)
    {
        MessageContent = messageContent;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton, messageBoxResult);
    }
    
    public MessageDialog(string messageContent, string caption, MessageBoxButton messageBoxButton, MessageBoxResult messageBoxResult)
    {
        MessageContent = messageContent;
        Caption = caption;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton, messageBoxResult);
    }
    
    public MessageDialog(string messageContent, MessageBoxButton messageBoxButton, Uri messageBoxImage, MessageBoxResult messageBoxResult)
    {
        MessageContent = messageContent;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton, messageBoxResult);
    }
    
    public MessageDialog(string messageContent, string caption, MessageBoxButton messageBoxButton, Uri messageBoxImage, MessageBoxResult messageBoxResult)
    {
        MessageContent = messageContent;
        Caption = caption;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
        
        SetButtonVisibility(messageBoxButton, messageBoxResult);
    }

    private void SetButtonVisibility(MessageBoxButton messageBoxButton, MessageBoxResult? messageBoxResult=null)
    {
        var buttonsStrings = messageBoxButton.ToString().SplitByMaj();
        
        Console.WriteLine(MessageBox.Resources.MessageBox_Yes);
        foreach (var buttonString in buttonsStrings)
        {
            var button = (Button)FindName($"Button{buttonString}")!;
            button.Visibility = Visibility.Visible;
        }
    }
}

public enum MessageBoxResult
{
    Ok,
    YesNo,
    OkCancel,
    YesNoCancel,
    Cancel
}