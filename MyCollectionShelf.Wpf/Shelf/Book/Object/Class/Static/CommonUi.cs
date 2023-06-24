using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MyCollectionShelf.Sql;
using MyCollectionShelf.Wpf.Shelf.Book.Object.Enum;
using SQLite;

namespace MyCollectionShelf.Wpf.Shelf.Book.Object.Class.Static;

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
    
    public static void SetCollection<T>(this ICollection<T> collection, ISQLiteConnection connection) where T : class, new()
    {
        var tableAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute)).First();
        
        var list = connection.Query<T>($"SELECT * FROM {tableAttribute.Name}");
        foreach (var lst in list)
        {
            collection.Add(lst);
        }
    }

    public static IEnumerable<T> SetCollection<T>() where T : class, new()
    {
        using var sqlHandler = new SqlMainHandler();
        var db = sqlHandler.GetSqlConnection();
        
        var tableAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute)).First();
        
        var list = db.Query<T>($"SELECT * FROM {tableAttribute.Name}");
        return list;
    }
    
    public static void UpdateCollection<T>(this ObservableCollection<T> collection, ComboBox comboBox) where T : class, new()
    {
        var selected = (T)comboBox.SelectedItem!;
        
        var listBoxItem = comboBox.FindParent<ListBoxItem>();
        var listBox = listBoxItem!.FindParent<ListBox>();
        
        var index = listBox!.ItemContainerGenerator.IndexFromContainer(listBoxItem!);
        
        collection[index] = selected;
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