using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.Drawer;

namespace TagCloud.CloudGenerator;

public class RectanglesGenerator(ICloudLayouter cloudLayouter, DrawerSettings drawerSettings) : IRectanglesGenerator
{
    private readonly List<WordInShape> _wordsInShape = new();

    public IList<WordInShape> GetWordsInShape(IDictionary<string, int> words)
    {
        foreach (var word in words)
        {
            var current = word.Key;
            var size = GenerateRectangleSize(word, words.Count);
            var rectangle = cloudLayouter.PutNextRectangle(size);
            var fontSize = GenerateFontSize(rectangle, current);
            _wordsInShape.Add(new WordInShape(current, rectangle, fontSize));
        }

        return _wordsInShape;
    }

    private float GenerateFontSize(Rectangle rectangle, string word)
    {
        var fontSizeWidth = rectangle.Width / word.Length;
        var fontSizeHeight = rectangle.Height;
        return Math.Min(fontSizeWidth, fontSizeHeight);
    }

    private Size GenerateRectangleSize(KeyValuePair<string, int> word, int count)
    {
        var length = word.Key.Length;
        var value = word.Value;
        var width = (int) (length * value * 0.1 * drawerSettings.CloudSize.Width / count);
        var height = (int) (value * 0.1 * drawerSettings.CloudSize.Height / count);
        return new Size(Math.Max(width, 5), Math.Max(height, 5));
    }
}