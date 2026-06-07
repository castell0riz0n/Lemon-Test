namespace Lemon_Test.Core.FileSystem;

/// <summary>
/// File manager for resource management demos
/// </summary>
public class FileManager
{
    private readonly string _baseDirectory;

    public FileManager(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
    }

    public void CreateFile(string fileName, string content)
    {
        var filePath = Path.Combine(_baseDirectory, fileName);
        File.WriteAllText(filePath, content);
    }

    public string ReadFile(string fileName)
    {
        var filePath = Path.Combine(_baseDirectory, fileName);
        return File.ReadAllText(filePath);
    }

    public void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_baseDirectory, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public bool FileExists(string fileName)
    {
        var filePath = Path.Combine(_baseDirectory, fileName);
        return File.Exists(filePath);
    }
}
