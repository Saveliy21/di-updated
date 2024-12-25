namespace TagCloud.FileReader;

public interface IFileReader
{
    public IEnumerable<string> TryReadFile(string filePath);
}