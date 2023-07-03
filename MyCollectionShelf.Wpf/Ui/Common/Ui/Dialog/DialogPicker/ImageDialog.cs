using System.Collections.Generic;
using System.Linq;

namespace MyCollectionShelf.Wpf.Ui.Common.Ui.Dialog.DialogPicker;

public class ImageDialog : IDialog
{
    public IEnumerable<string> Extension => new[] { ".bmp", ".ico", ".jpg", ".jpeg", ".png" };
    
    public string Filter { get; }

    private readonly FileDialog _fileDialog;

    public ImageDialog()
    {
        var lst = string.Join(';', Extension.Select(ext => $"*{ext}"));
        Filter = $"Fichier image ({lst})|{lst}";

        _fileDialog = new FileDialog(Filter, false, "Merci de choisir une image");
    }

    public string? GetFile() => _fileDialog.GetFile();

    public string? SaveFile() => _fileDialog.SaveFile();
}