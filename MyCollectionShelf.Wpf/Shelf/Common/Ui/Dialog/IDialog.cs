using System.Collections.Generic;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog;

public interface IDialog
{
    public IEnumerable<string>? Extension { get; }
    
    public string Filter { get; }
    
    public string? GetFile();

    public string? SaveFile();
}