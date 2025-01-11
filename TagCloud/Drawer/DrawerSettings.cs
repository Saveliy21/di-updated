using System.Drawing;
using TagCloud.CloudLayout;
using TagCloud.ColoringAlgorithms;

namespace TagCloud.Drawer;

public record DrawerSettings(IColorAlgorithm WordsColor, Size CloudSize, string Font);