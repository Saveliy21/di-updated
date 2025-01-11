using System.Drawing;

namespace TagCloud.ColoringAlgorithms;

public class GradientColor(Color gradientFrom, Color gradientTo) : IColorAlgorithm
{
    public Color[] GetColors(int count)
    {
        return Enumerable.Range(0, count)
            .Select(i => Color.FromArgb(
                (int) (gradientFrom.R + (gradientTo.R - gradientFrom.R) * (float) i / (count - 1)),
                (int) (gradientFrom.G + (gradientTo.G - gradientFrom.G) * (float) i / (count - 1)),
                (int) (gradientFrom.B + (gradientTo.B - gradientFrom.B) * (float) i / (count - 1))
            ))
            .ToArray();
    }
}