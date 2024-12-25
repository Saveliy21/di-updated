using System.Drawing;

namespace TagCloud.ColoringAlgorithms;

public class GradientColor(Color[] gradient) : IColorAlgorithm
{
    private readonly Color _fromColor = gradient[0];
    private readonly Color _toColor = gradient[1];

    public Color[] GetColors(int count)
    {
        return Enumerable.Range(0, count)
            .Select(i => Color.FromArgb(
                (int) (_fromColor.R + (_toColor.R - _fromColor.R) * (float) i / (count - 1)),
                (int) (_fromColor.G + (_toColor.G - _fromColor.G) * (float) i / (count - 1)),
                (int) (_fromColor.B + (_toColor.B - _fromColor.B) * (float) i / (count - 1))
            ))
            .ToArray();
    }
}