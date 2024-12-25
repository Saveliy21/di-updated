using System.Drawing;

namespace TagCloud.CloudLayout;

public interface ICloudLayouter
{
    public Rectangle PutNextRectangle(Size rectangleSize);
}