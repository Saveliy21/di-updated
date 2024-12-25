namespace TagCloud.CloudGenerator;

public interface IRectanglesGenerator
{
    public IList<WordInShape> GetWordsInShape(IDictionary<string, int> words);
}