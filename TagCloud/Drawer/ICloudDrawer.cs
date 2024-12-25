using System.Drawing;
using TagCloud.TextPreparator;

namespace TagCloud.Drawer;

public interface ICloudDrawer
{
    public void DrawTagsCloudFromFile(string filepath);
}