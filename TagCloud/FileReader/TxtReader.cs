namespace TagCloud.FileReader;

public class TxtReader : IFileReader
{
    public IEnumerable<string> TryReadFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        return File.ReadLines(filePath).ToList();
    }
}