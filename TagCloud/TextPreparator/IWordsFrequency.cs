using TagCloud.FileReader;

namespace TagCloud.TextPreparator;

public interface IWordsFrequency
{
    public IDictionary<string, int> GetWordsFrequencyFromFile(string fileName);
}