using System.Drawing;

namespace TagCloud.ColoringAlgorithms;

public class RandomColor : IColorAlgorithm
{
    public Color[] GetColors(int count)
    {
        Random random = new Random();

        return Enumerable.Range(0, count)
            .Select(_ => Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256)))
            .ToArray();
    }
}