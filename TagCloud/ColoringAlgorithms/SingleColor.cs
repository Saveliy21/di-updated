using System.Drawing;

namespace TagCloud.ColoringAlgorithms;

public class SingleColor(Color color) : IColorAlgorithm
{
    public Color[] GetColors(int count) => Enumerable.Repeat(color, count).ToArray();
}