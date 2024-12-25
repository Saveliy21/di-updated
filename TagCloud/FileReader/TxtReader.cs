namespace TagCloud.FileReader;

public class TxtReader : IFileReader
{
    public IEnumerable<string> TryReadFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        return File.ReadLines(filePath)
            .Select(s => s.Trim())
            .Select(s => s.ToLower())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();
    }
}