namespace Lemon_Test.Core.ParallelExecution;

/// <summary>
/// A simple file manager that demonstrates resource conflicts in parallel execution
/// </summary>
public class FileManager
{
    public void CreateFile(string fileName, string content)
    {
        File.WriteAllText(fileName, content);
    }

    public string ReadFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    public bool FileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    public void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    public void ProcessFiles(string[] fileNames)
    {
        foreach (var fileName in fileNames)
        {
            CreateFile(fileName, $"Processed content for {fileName}");
            Thread.Sleep(100); // Simulate processing time
        }
    }
}
