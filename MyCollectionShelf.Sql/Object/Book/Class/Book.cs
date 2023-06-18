using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCollectionShelf.Sql.Object.Book.Class;

public class Book : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private BookInformations _bookInformations = new();

    public BookInformations BookInformations
    {
        get => _bookInformations;
        set
        {
            _bookInformations = value;
            OnPropertyChanged();
        }
    }

    private BookNote _bookNote = new();

    public BookNote BookNote
    {
        get => _bookNote;
        set
        {
            _bookNote = value;
            OnPropertyChanged();
        }
    }
}