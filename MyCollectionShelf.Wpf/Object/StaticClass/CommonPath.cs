using System;
using System.IO;

namespace MyCollectionShelf.Wpf.Object.StaticClass;

public static class CommonPath
{
    public static string GetCurrentPath() => AppDomain.CurrentDomain.BaseDirectory;

    public static string GetTemporaryCoverFilePath() => Path.Join(GetCurrentPath(), "FolderScan", "tempScan.jpg");
}