using System.Drawing;

namespace TagCloud.ColoringAlgorithms;

public interface IColorAlgorithm
{
    public Color[] GetColors(int count);
}