using System.Collections.Generic;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog.DialogPicker;

public interface IDialog
{
    public IEnumerable<string>? Extension { get; }
    
    public string Filter { get; }
    
    public string? GetFile();

    public string? SaveFile();
}