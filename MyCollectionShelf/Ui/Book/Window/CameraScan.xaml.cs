using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using MyCollectionShelf.Book;
using MyCollectionShelf.Book.Object.Static_Class;
using MyCollectionShelf.Camera.Object.Static_Class;
using MyCollectionShelf.WebApi.Object.Book.Enum;
using ZXing;
using ZXing.Common;

namespace MyCollectionShelf.Ui.Book.Window;

public partial class CameraScan : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private DecodingOptions _decodingOption = new();

    public DecodingOptions DecodingOptions
    {
        get => _decodingOption;
        set
        {
            _decodingOption = value;
            OnPropertyChanged();
        }
    }

    private MyCollectionShelf.Book.Object.Class.Book _book = new();

    public MyCollectionShelf.Book.Object.Class.Book Book
    {
        get => _book;
        set
        {
            _book = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> IsbnFounds { get; } = new();

    public CameraScan()
    {
        IsbnFounds.Add("9782072934902");
        InitializeComponent();

        DecodingOptions = new DecodingOptions
        {
            TryHarder = true,
            PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.EAN_13 }
        };

        VideoPreview.ResultFound += VideoPreviewResultFound;

        var devices = CameraHelper.GetAvailableCameras();
        VideoPreview.StartCamera(devices.First());
    }

    private void VideoPreviewResultFound(Camera.Ui.Camera sender, Result result)
    {
        if (result.BarcodeFormat != BarcodeFormat.EAN_13) return;

        if (!IsbnFounds.Contains(result.Text)) IsbnFounds.Add(result.Text);
    }

    private async void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var isbn = ((Label)sender).Content as string;
        Console.WriteLine(isbn);
        
        // todo à finir
        
        var api = new BookAllApi();
        Book = await api.GetBookInformation(isbn!);
        
        var uri = await Book.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, "test.jpg");
        
        
        Console.WriteLine(uri);
        Console.WriteLine(Book.BookInformations.Title);
        DialogResult = true;

        // var api = new OpenLibraryApi();
        //
        // var book = await api.GetBookInformation(result.Text);
        //
        // var success = await book?.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge,
        //     $"{book.BookInformations.Title}.jpg")!;
        //
        // Console.WriteLine($"cover download {success}");
    }
}