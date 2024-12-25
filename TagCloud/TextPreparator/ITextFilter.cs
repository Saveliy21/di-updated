namespace TagCloud.TextPreparator;

public interface ITextFilter
{
    public IEnumerable<string> GetFilteredText(IEnumerable<string> words);
}