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
        
        SetButtonVisibility(MessageBoxButton.OK);
    }
    
    public MessageDialog(string messageContent, string caption)
    {
        MessageContent = messageContent;
        Caption = caption;
        
        InitializeComponent();
        
        SetButtonVisibility(MessageBoxButton.OK);
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
        
        SetButtonVisibility(MessageBoxButton.OK);
    }
    
    public MessageDialog(string messageContent, string caption, Uri messageBoxImage)
    {
        MessageContent = messageContent;
        Caption = caption;
        MessageBoxImage = messageBoxImage;
        
        InitializeComponent();
        
        SetButtonVisibility(MessageBoxButton.OK);
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

    private void SetButtonVisibility(MessageBoxButton messageBoxButton, MessageBoxResult messageBoxResult = MessageBoxResult.Cancel)
    {
        MessageBoxResult = messageBoxResult;
        
        var buttonsString = messageBoxButton.ToString().SplitByMaj();
        
        foreach (var buttonString in buttonsString)
        {
            var button = (Button)FindName($"Button{buttonString}")!;
            button.Content = MessageBox.Resources.ResourceManager.GetString(buttonString);
            button.Visibility = Visibility.Visible;
        }
    }
}