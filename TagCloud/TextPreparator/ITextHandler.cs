using TagCloud.FileReader;

namespace TagCloud.TextPreparator;

public interface ITextHandler
{
    public IDictionary<string, int> HandleText(string fileName);
}