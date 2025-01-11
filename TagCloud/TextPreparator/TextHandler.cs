using TagCloud.FileReader;

namespace TagCloud.TextPreparator;

public class TextHandler(ITextFilter textFilter, IFileReader fileReader) : IWordsFrequency
{
    private readonly Dictionary<string, int> _wordCount = new();

    private IDictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
    {
        foreach (var word in words)
        {
            if (!_wordCount.TryAdd(word, 1))
                _wordCount[word]++;
        }

        return _wordCount;
    }

    public IDictionary<string, int> GetWordsFrequencyFromFile(string fileName)
    {
        var words = fileReader.TryReadFile(fileName);
        var filteredWords = textFilter.GetFilteredText(words);
        return GetWordsFrequency(filteredWords);
    }
}