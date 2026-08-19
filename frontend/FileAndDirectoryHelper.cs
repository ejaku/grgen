using System;
using System.IO;

public static class FileAndDirectoryHelper
{
    public static bool AreDirectoriesTheSame(DirectoryInfo _this, DirectoryInfo that)
    {
        return _this.FullName.Equals(that.FullName, StringComparison.CurrentCultureIgnoreCase); // this assumens FullName resolves a shortcut to a full path, maybe only Path.GetFullPath does, maybe TODO: path separators could be different (Path.DirectorySeparatorChar and alternative), ending could be with separator or without
    }

    public static FileInfo GetFileInfo(DirectoryInfo parent, string child)
    {
        return new FileInfo(Path.Combine(parent.FullName, child));
    }

    public static DirectoryInfo GetDirectoryInfo(DirectoryInfo parent, string child)
    {
        return new DirectoryInfo(Path.Combine(parent.FullName, child));
    }

    public static void Mkdirs(DirectoryInfo path)
    {
        if(!path.Exists)
            path.Create();
    }
}
