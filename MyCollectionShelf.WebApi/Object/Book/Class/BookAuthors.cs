using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.WebApi.Object.Book.Class;

public class BookAuthors : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    private string _familyName = string.Empty;

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

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }
    
    private string _role = string.Empty;

    public string Role
    {
        get => _role;
        set
        {
            _role = value;
            OnPropertyChanged();
        }
    }

}