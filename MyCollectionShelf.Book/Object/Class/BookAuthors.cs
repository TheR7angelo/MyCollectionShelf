﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace MyCollectionShelf.Book.Object.Class;

[Table("book_author")]
public class BookAuthors : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private long _id;

    [PrimaryKey, AutoIncrement, Column("id")]
    public long Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }
    
    private string _familyName = string.Empty;

    [Column("family_name")]
    public string FamilyName
    {
        get => _familyName;
        set
        {
            _familyName = value;
            OnPropertyChanged();
        }
    }
    
    private string _name = string.Empty;

    [Column("name")]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }
    
    // private string _role = string.Empty;
    //
    // public string Role
    // {
    //     get => _role;
    //     set
    //     {
    //         _role = value;
    //         OnPropertyChanged();
    //     }
    // }
    
    public string NameConcat => $"{FamilyName}, {Name}";

}