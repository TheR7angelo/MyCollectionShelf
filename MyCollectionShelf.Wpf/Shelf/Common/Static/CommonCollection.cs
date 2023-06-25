using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Sql.Object.Book.Class.View;
using MyCollectionShelf.Wpf.Shelf.Common.Enum;
using SQLite;

namespace MyCollectionShelf.Wpf.Shelf.Common.Static;

public static class CommonCollection
{
    public static void ToCoverFullPath(this IEnumerable<VBookShelf> bookShelves)
    {
        foreach (var bookShelf in bookShelves)
        {
            bookShelf.BookSeriesCover = Path.GetFullPath(bookShelf.BookSeriesCover);
        }
    }
    
    public static void UpdateCollection<T>(this ObservableCollection<T> collection, ComboBox comboBox) where T : class, new()
    {
        var selected = (T)comboBox.SelectedItem!;
        
        var listBoxItem = comboBox.FindParent<ListBoxItem>();
        var listBox = listBoxItem!.FindParent<ListBox>();
        
        var index = listBox!.ItemContainerGenerator.IndexFromContainer(listBoxItem!);
        
        collection[index] = selected;
    }
    
    public static void GetCollection<T>(this ICollection<T> collection, ISQLiteConnection connection) where T : class, new()
    {
        var tableAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute)).First();
        
        var list = connection.Query<T>($"SELECT * FROM {tableAttribute.Name}");
        foreach (var lst in list)
        {
            collection.Add(lst);
        }
    }

    public static IEnumerable<T> GetCollection<T>() where T : class, new()
    {
        using var sqlHandler = new SqlMainHandler();
        var db = sqlHandler.GetSqlConnection();
        
        var tableAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute)).First();
        
        var list = db.Query<T>($"SELECT * FROM {tableAttribute.Name}");
        return list;
    }
    
    public static void AddRemoveList<T>(this object collection, EAddRemove function, T item, T newItem)
    {
        var zcollection = (ObservableCollection<T>)collection;

        switch (function)
        {
            case EAddRemove.Add:
                zcollection.Add(newItem);
                break;
            case EAddRemove.Remove when zcollection.Count > 1:
                zcollection.Remove(item);
                break;
            default:
                break;
        }
    }
}