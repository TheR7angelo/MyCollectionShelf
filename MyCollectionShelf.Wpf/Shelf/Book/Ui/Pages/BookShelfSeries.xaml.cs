﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class.Table;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Book.Ui.CustomButton;
using MyCollectionShelf.Wpf.Shelf.Common.Comparator;
using MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog.DialogPicker;
using MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog.MessageBox;
using SQLite;

namespace MyCollectionShelf.Wpf.Shelf.Book.Ui.Pages;

public partial class BookShelfSeries
{
    public static readonly DependencyProperty VBookShelfProperty = DependencyProperty.Register(nameof(VBookShelf),
        typeof(VBookShelf), typeof(BookShelfSeries), new PropertyMetadata(default(VBookShelf), VBookShelfPropertyChanged_Callback));

    private static void VBookShelfPropertyChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var sender = (BookShelfSeries)d;
        sender.VBookShelfOriginal = sender.VBookShelf.DeepCopy();
    }

    public static readonly DependencyProperty BookInformationsProperty =
        DependencyProperty.Register(nameof(BookInformations), typeof(ObservableCollection<BookInformations>),
            typeof(BookShelfSeries), new PropertyMetadata(default(ObservableCollection<BookInformations>)));

    private VBookShelf? VBookShelfOriginal { get; set; }
    
    public BookShelfSeries()
    {
        InitializeComponent();
    }

    public VBookShelf VBookShelf
    {
        get => (VBookShelf)GetValue(VBookShelfProperty);
        init => SetValue(VBookShelfProperty, value);
    }

    public ObservableCollection<BookInformations> BookInformations
    {
        get => (ObservableCollection<BookInformations>)GetValue(BookInformationsProperty);
        set => SetValue(BookInformationsProperty, value);
    }

    private void ButtonShelfTome_OnClick(object sender, RoutedEventArgs e)
    {
        var button = (ButtonShelfTome)sender;
        Console.WriteLine(button.BookInformations.TomeNumber);
    }

    private void ImageCover_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (!(bool)ToggleEdit.IsChecked!) return;

        var dialog = new ImageDialog();
        var file = dialog.GetFile();

        if (string.IsNullOrEmpty(file)) return;

        VBookShelf.BookSeriesCover = file;
    }

    private async void ToggleEdit_OnUnchecked(object sender, RoutedEventArgs e)
    {
        var differences = VBookShelfOriginal.Compare(VBookShelf);

        if (differences.Count.Equals(0)) return;
        
        var messageDialog = new MessageDialog(BookShelfSeriesResources.EditConfirmationContent, BookShelfSeriesResources.EditConfirmationCaption, MessageBoxButton.YesNoCancel);
        await DialogHost.Show(messageDialog, DialogHost.Identifier!);
        
        var result = messageDialog.MessageBoxResult;

        if (result != MessageBoxResult.Yes) return;
        
        var vBookShelfType = typeof(VBookShelf);
        
        var imageCoverName = vBookShelfType.GetProperty(nameof(VBookShelf.BookSeriesCover))!.Name;
        var titleName = vBookShelfType.GetProperty(nameof(VBookShelf.BookSeriesTitle))!.Name;
        var seriesSummarizeName = vBookShelfType.GetProperty(nameof(VBookShelf.SeriesSummarize))!.Name;
        
        var bookSeriesType = typeof(BookSeries);
        var idColumn = bookSeriesType.GetProperty(nameof(BookSeries.Id))!.GetCustomAttribute<ColumnAttribute>()!.Name;
        string column;
        
        foreach (var propertiesInfoName in differences.Select(difference => typeof(VBookShelf).GetProperty(difference.Property)!.Name))
        {
            string? cmd = null;
            
            if (propertiesInfoName.Equals(imageCoverName))
            {
                Console.WriteLine("Copy image require");
            }
            else if (propertiesInfoName.Equals(titleName))
            {
                column = bookSeriesType.GetProperty(nameof(BookSeries.Title))!.GetCustomAttribute<ColumnAttribute>()!.Name;
                cmd = $"UPDATE book_series SET {column} = {VBookShelf.BookSeriesTitle.ToSql()} WHERE {idColumn} = {VBookShelfOriginal!.BookSeriesId};";
            }
            else if (propertiesInfoName.Equals(seriesSummarizeName))
            {
                column = bookSeriesType.GetProperty(nameof(BookSeries.SeriesSummarize))!.GetCustomAttribute<ColumnAttribute>()!.Name;
                cmd = $"UPDATE book_series SET {column} = {VBookShelf.SeriesSummarize.ToSql()} WHERE {idColumn} = {VBookShelfOriginal!.BookSeriesId};";
            }

            if (cmd is null) continue;
            
            using var sqlHander = new SqlMainHandler();
            using var connection = sqlHander.GetSqlConnection();
            connection.Execute(cmd);
        }
    }
}