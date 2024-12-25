using System.Drawing;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudGenerator;
using TagCloud.CloudLayout;
using TagCloud.ColoringAlgorithms;
using TagCloud.Drawer;

namespace TagCloudTests;

public class CloudGeneratorTests
{
    private ICloudLayouter cloudLayouter;
    private IColorAlgorithm colorAlgorithm;
    private DrawerSettings drawerSettings;
    private RectanglesGenerator rectanglesGenerator;
    private static Rectangle _rectangle = new Rectangle(0, 0, 10, 10);

    [SetUp]
    public void Setup()
    {
        cloudLayouter = A.Fake<ICloudLayouter>();
        colorAlgorithm = A.Fake<IColorAlgorithm>();
        drawerSettings = new DrawerSettings(colorAlgorithm, new Size(100, 100), "Georgia");
        rectanglesGenerator = new RectanglesGenerator(cloudLayouter, drawerSettings);
    }

    [Test]
    public void GetWordsInShape_ShouldReturnCorrectResultSize()
    {
        A.CallTo(() => cloudLayouter.PutNextRectangle(new Size(5, 5)))
            .Returns(new Rectangle(0, 0, 5, 5));
        var dict = new Dictionary<string, int>
        {
            {"собака", 1},
            {"кошка", 2},
            {"попугай", 3}
        };
        var expected = 3;

        var actual = rectanglesGenerator.GetWordsInShape(dict).Count;

        actual.Should().Be(expected);
    }

    [Test]
    public void GetWordsInShape_ShouldReturnCorrectResult()
    {
        A.CallTo(() => cloudLayouter.PutNextRectangle(new Size(30, 5)))
            .Returns(_rectangle);
        A.CallTo(() => cloudLayouter.PutNextRectangle(new Size(50, 10)))
            .Returns(_rectangle);

        var words = new Dictionary<string, int>
        {
            {"собака", 1},
            {"кошка", 2},
        };
        var expected = new List<WordInShape>
        {
            new("собака", _rectangle, 1),
            new("кошка", _rectangle, 2),
        };

        var actual = rectanglesGenerator.GetWordsInShape(words);

        actual.Should().BeEquivalentTo(expected);
    }
}