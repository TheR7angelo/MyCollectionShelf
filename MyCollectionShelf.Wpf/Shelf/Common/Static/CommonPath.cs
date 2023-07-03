using System;
using System.IO;

namespace MyCollectionShelf.Wpf.Shelf.Common.Static;

public static class CommonPath
{
    public static string GetCurrentPath() => AppDomain.CurrentDomain.BaseDirectory;

    public static string GetTemporaryCoverFilePath() => Path.Join(GetCurrentPath(), "FolderScan", "tempScan.jpg");
}