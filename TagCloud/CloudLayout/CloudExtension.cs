using System.Drawing;
using TagCloud.CloudGenerator;

namespace TagCloud.CloudLayout;

public static class CloudExtension
{
    public static Size GetCloudSize(this IList<WordInShape> words)
    {
        var left = words.Min(x => x.Rectangle.Left);
        var right = words.Max(x => x.Rectangle.Right);
        var top = words.Min(x => x.Rectangle.Top);
        var bottom = words.Max(x => x.Rectangle.Bottom);
        var size = new Size( Math.Abs(right) + Math.Abs(left),Math.Abs(bottom) + Math.Abs(top));
        return size;
    }
}