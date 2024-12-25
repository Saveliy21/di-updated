using System.Drawing;
using TagCloud.CloudForms;
using TagCloud.Drawer;

namespace TagCloud.CloudLayout;

public class CloudLayouter(ICloudForm cloudForm) : ICloudLayouter
{
    public readonly List<Rectangle> Rectangles = new();

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
        {
            throw new ArgumentException($"Rectangle size ({rectangleSize}) should be positive");
        }

        do
        {
            Point point = cloudForm.GetNextPoint();
            point.Offset(-rectangleSize.Width / 2, -rectangleSize.Height / 2);
            rectangle = new Rectangle(point, rectangleSize);
        } while (Rectangles.Any(r => r.IntersectsWith(rectangle)));

        Rectangles.Add(rectangle);
        return rectangle;
    }
}