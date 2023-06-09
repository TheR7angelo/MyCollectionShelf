﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using MyCollectionShelf.Camera.Object.Static_Class;
using ZXing;
using ZXing.Common;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Window;

public partial class CameraScan : INotifyPropertyChanged, IDisposable
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

    private string _isbn = string.Empty;

    public string Isbn
    {
        get => _isbn;
        set
        {
            _isbn = value;
            OnPropertyChanged();
        }
    }
    
    // private MyCollectionShelf.Book.Object.Class.Book _book = new();
    //
    // public MyCollectionShelf.Book.Object.Class.Book Book
    // {
    //     get => _book;
    //     set
    //     {
    //         _book = value;
    //         OnPropertyChanged();
    //     }
    // }

    public ObservableCollection<string> IsbnFounds { get; } = new();

    public CameraScan()
    {
        IsbnFounds.Add("9782072934902");
        IsbnFounds.Add("9782380713084");
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

    private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        var isbn = (string)((Label)sender).Content;

        Isbn = isbn;
        
        Console.WriteLine(isbn);
        
        // todo à finir
        
        // var api = new BookAllApi();
        // Book = await api.GetBookInformation(isbn!);
        //
        // var tempPicture = CommonPath.GetTemporaryCoverFilePath();
        // var sucess = await Book.BookInformations.BookCover.DownloadCover(EBookCoverSize.ExtraLarge, tempPicture);
        //
        // if (sucess)
        // {
        //     Book.BookInformations.BookCover!.Storage = new Uri(tempPicture);
        // }
        //
        // Console.WriteLine(Book.BookInformations.Title);
        
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

    public void Dispose()
    {
        VideoPreview?.Dispose();
    }
}