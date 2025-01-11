using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.ColoringAlgorithms;

namespace TagCloudTests;

public class ColoringAlgorithmsTests
{
    private readonly GradientColor _gradientColor = new (Color.Red, Color.Blue);
    private readonly RandomColor _randomColor = new ();
    private readonly SingleColor _singleColor = new (Color.Green);

    [Test]
    public void GetColors_ShouldReturnIdenticalColors_WithGradientAlgorithm()
    {
        var expected = new[]
        {
            Color.FromArgb(255, 0, 0),
            Color.FromArgb(0, 0, 255)
        };

        var actual = _gradientColor.GetColors(2);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetColors_ShouldReturnCorrectSize_WithGradientAlgorithm()
    {
        var expected = 267;

        var actual = _gradientColor.GetColors(267);

        actual.Length.Should().Be(expected);
    }
    [Test]
    public void GetColors_ShouldReturnCorrectSize_WhithRandomAlgorithm()
    {
        var expected = 267;

        var actual = _randomColor.GetColors(267);

        actual.Length.Should().Be(expected);
    }
    
    [Test]
    public void GetColors_ShouldReturnCorrectSize_WhithSingleAlgorithm()
    {
        var expected = 267;

        var actual = _singleColor.GetColors(267);

        actual.Length.Should().Be(expected);
    }
    
    [Test]
    public void GetColors_ShouldReturnCorrectColors_WhithSingleAlgorithm()
    {
        var expected = Color.Green;

        var actual = _singleColor.GetColors(50);
        
        actual.All(color => color.Equals(expected)).Should().BeTrue();
    }
    
}