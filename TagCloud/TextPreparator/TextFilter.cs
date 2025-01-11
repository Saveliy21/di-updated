namespace TagCloud.TextPreparator;

public class TextFilter : ITextFilter
{
    private static readonly HashSet<string> BoringWords = new()
    {
        "а", "и", "в", "на", "с", "по", "для", "о", "как", "к", "из", "когда", "что", "но", "не", "бы", "же", "только",
        "из-за", "из-под", "около", "вокруг", "перед", "возле",
        "он", "она", "оно", "они", "им", "ей", "ему", "её", "его", "их"
    };

    public IEnumerable<string> GetFilteredText(IEnumerable<string> words)
    {
        var filteredText = words
            .Select(s => s.Trim())
            .Select(s => s.ToLower())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Where(word => !BoringWords.Contains(word));

        return filteredText;
    }
}