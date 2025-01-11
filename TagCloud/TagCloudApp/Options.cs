﻿using CommandLine;

namespace TagCloud.TagCloudApp;

public class Options
{
    [Option('f', "file",Required = true, HelpText = "Путь к исходному текстовому файлу.")]
    public string FilePath { get; set; }

    [Option('a', "algorithm", Default = "circular", HelpText = "Алгоритм построение облака (circular или square)")]
    public string Algorithm { get; set; }

    [Option('w', "width", Default = 1000, HelpText = "Размер облака тегов")]
    public int Size { get; set; }

    [Option('c', "WordsColor", Default = "random", HelpText = "Алгоритм расцветки " +
                                                           "единый цвет для всех слов, random или " +
                                                           "градиент - указать два цвета (от-до)")]
    public string WordsColor { get; set; }

    [Option('t', "font", Default = "Times New Roman", HelpText = "Шрифт для текста.")]
    public string Font { get; set; }
}