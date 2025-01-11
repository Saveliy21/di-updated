using System.Drawing;
using TagCloud.CloudGenerator;
using TagCloud.CloudLayout;
using TagCloud.TextPreparator;
using Color = System.Drawing.Color;

namespace TagCloud.Drawer;

public class RectanglesCloudDrawer(
    DrawerSettings drawerSettings,
    IWordsFrequency wordsFrequency,
    IRectanglesGenerator rectanglesGenerator)
    : ICloudDrawer
{
    private Bitmap DrawCloud(IList<WordInShape> words, Size size)
    {
        var bmp = new Bitmap(size.Width, size.Height);
        using var graphics = Graphics.FromImage(bmp);

        var bgColor = Color.White;
        graphics.Clear(bgColor);
        var colors = drawerSettings.WordsColor.GetColors(words.Count);
        var rectBrush = new SolidBrush(bgColor);
        var i = 0;
        foreach (var (word, rect, fontSize) in words)
        {
            var textBrush = new SolidBrush(colors[i++]);
            graphics.FillRectangle(rectBrush, rect);
            var font = new Font(drawerSettings.Font, fontSize);

            var stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(word, font, textBrush, rect, stringFormat);
        }

        return bmp;
    }

    public void DrawTagsCloudFromFile(string filepath)
    {
        var words = wordsFrequency.GetWordsFrequencyFromFile(filepath);
        var wordsInShape = rectanglesGenerator.GetWordsInShape(words);
        var cloudSize = wordsInShape.GetCloudSize();
        var result = DrawCloud(wordsInShape, cloudSize);
        SaviorImages.SaveImage(result, "TagCloud", "PNG");
    }
}