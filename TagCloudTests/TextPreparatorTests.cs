using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.FileReader;
using TagCloud.TextPreparator;
using static System.IO.File;

namespace TagCloudTests;

public class TextPreparatorTests
{
    private const string TempFileName = "temp.txt";
    private static readonly TxtReader TxtReader = new();
    private static readonly TextFilter TextFilter = new();
    private static readonly TextHandler TextHandler = new TextHandler(TextFilter, TxtReader);

    [TearDown]
    public void TearDown()
    {
        if (Exists(TempFileName))
        {
            Delete(TempFileName);
        }
    }

    [Test]
    public void TryReadFile_ShouldThrowException_WithNotExistedFile()
    {
        var invalidFilePath = "NotExistedfile.txt";

        Action act = () => TxtReader.TryReadFile(invalidFilePath);

        act.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void TryReadFile_ShouldReduceToLowerCase_WhenReadingFile()
    {
        var words = new[]
        {
            "Рим",
            "Сава",
            "ШлЯпА"
        };
        WriteAllLines(TempFileName, words);
        var expected = new[]
        {
            "рим",
            "сава",
            "шляпа"
        };


        var actual = TxtReader.TryReadFile(TempFileName);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void TryReadFile_ShouldIgnoreSpaces_WhenReadingFile()
    {
        var words = new[]
        {
            "   кот",
            "стены    ",
            "   грыз   ",
            "    ",
            ""
        };
        WriteAllLines(TempFileName, words);
        var expected = new[]
        {
            "кот",
            "стены",
            "грыз"
        };


        var actual = TxtReader.TryReadFile(TempFileName);

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetFilteredText_ShouldRemove_WhenTextContainsBoringWords()
    {
        var words = new List<string> {"когда", "собака", "гуляет", "на"};
        var expected = new List<string> {"собака", "гуляет"};

        var actual = TextFilter.GetFilteredText(words).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetFilteredText_ShouldReturnSameText_WhenNoBoringWords()
    {
        var expected = new List<string> {"собака", "гуляет"};

        var actual = TextFilter.GetFilteredText(expected).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void HandleText_ShouldReturnCorrectWordFrenquency()
    {
        var words = new[] {"собака", "гуляет", "собака"};
        WriteAllLines(TempFileName, words);
        var expected = new Dictionary<string, int>
        {
            {"собака", 2},
            {"гуляет", 1}
        };

        var actual = TextHandler.GetWordsFrequencyFromFile(TempFileName);
        
        actual.Should().BeEquivalentTo(expected);
    }
}