using Microsoft.Win32;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Dialog;

public class FileDialog
{
    private readonly OpenFileDialog _openFileDialog = new();
    private readonly SaveFileDialog _saveFileDialog = new();
    
    public FileDialog(string filter, bool multiSelect, string title)
    {
        _openFileDialog.Filter = filter;
        _openFileDialog.Multiselect = multiSelect;
        _openFileDialog.Title = title;

        _saveFileDialog.Filter = filter;
        _saveFileDialog.Title = title;
    }

    public string? GetFile()
    {
        var result = _openFileDialog.ShowDialog();
        return result.Equals(true) ? _openFileDialog.FileName : null;
    }

    public string? SaveFile()
    {
        var result = _saveFileDialog.ShowDialog();
        return result.Equals(true) ? _saveFileDialog.FileName : null;
    }
}