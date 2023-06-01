﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Book.Object.Class;

public class BookNote : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    private bool _isRead;

    public bool IsRead
    {
        get => _isRead;
        set
        {
            _isRead = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _startDate;

    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }
    
    private DateTime? _endDate;

    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
        }
    }

    private decimal? _price;

    public decimal? Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    
    
}