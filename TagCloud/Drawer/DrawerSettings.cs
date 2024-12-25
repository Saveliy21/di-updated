using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.ColoringAlgorithms;

namespace TagCloud.Drawer;

public class DrawerSettings(IColorAlgorithm wordsColor, Size cloudSize, string font)
{
    public IColorAlgorithm WordsColor { get; } = wordsColor;
    public Size CloudSize { get; } = cloudSize;
    public string Font { get; } = font;
}