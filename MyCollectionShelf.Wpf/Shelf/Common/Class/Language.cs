using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Wpf.Shelf.Common.Class;

public class Language : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _displayName = string.Empty;

    public string DisplayName
    {
        get => _displayName;
        set
        {
            _displayName = value;
            OnPropertyChanged();
        }
    }
    
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

    public CultureInfo CultureInfo
    {
        get => _cultureInfo;
        set
        {
            _cultureInfo = value;
            OnPropertyChanged();
        }
    }
}