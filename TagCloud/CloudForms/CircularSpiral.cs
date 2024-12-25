using System.Drawing;

namespace TagCloud.CloudForms;

public class CircularSpiral(Point center) : ICloudForm
{
    private double _angle;
    private const double AngleStep = 0.01;

    public Point GetNextPoint()
    {
        _angle += AngleStep;
        var x = (int) (center.X + _angle * Math.Cos(_angle));
        var y = (int) (center.Y + _angle * Math.Sin(_angle));
        return new Point(x, y);
    }
}