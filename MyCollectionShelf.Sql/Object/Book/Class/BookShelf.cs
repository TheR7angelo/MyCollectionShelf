using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Sql.Object.Book.Class;

public class BookShelf : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    
    
}