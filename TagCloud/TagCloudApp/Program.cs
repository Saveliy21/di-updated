using System.Drawing;
using Autofac;
using CommandLine;
using TagCloud.CloudForms;
using TagCloud.CloudGenerator;
using TagCloud.CloudLayout;
using TagCloud.ColoringAlgorithms;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.TextPreparator;

namespace TagCloud.TagCloudApp;

public static class Program
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                Console.WriteLine($"Путь к файлу: {options.FilePath} " + Environment.NewLine +
                                  $"Алгоритм: {options.Algorithm}" + Environment.NewLine +
                                  $"Алгоритм расцветки слов: {options.WordsColor}" + Environment.NewLine +
                                  $"Шрифт: {options.Font}" + Environment.NewLine +
                                  $"Размер: {options.Size}");
                var container = ConfigureContainer(options);
                container.Resolve<IWordsFrequency>();
                var generator = container.Resolve<ICloudDrawer>();
                generator.DrawTagsCloudFromFile(options.FilePath);
            })
            .WithNotParsed(errors => { Console.WriteLine("Ошибка в параметрах командной строки."); });
    }

    private static IContainer ConfigureContainer(Options options)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<TxtReader>().As<IFileReader>();
        builder.RegisterType<TextFilter>().As<ITextFilter>();
        builder.RegisterType<TextHandler>().As<IWordsFrequency>();
        builder.Register(BuildColoringAlgorithm(options));
        builder.Register(BuildDrawerSettings(options));
        builder.Register(BuildCloudForm(options));
        builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<RectanglesGenerator>().As<IRectanglesGenerator>();
        builder.RegisterType<RectanglesCloudDrawer>().As<ICloudDrawer>();
        return builder.Build();
    }

    private static Func<IComponentContext, IColorAlgorithm> BuildColoringAlgorithm(Options options)
    {
        if (options.WordsColor.ToLower().Equals("random"))
        {
            return c => new RandomColor();
        }

        if (Color.FromName(options.WordsColor).IsKnownColor)
        {
            return c => new SingleColor(Color.FromName(options.WordsColor));
        }

        var gradient = options.WordsColor.Split("-").Select(Color.FromName).ToArray();
        return c => new GradientColor(gradient[0], gradient[1]);
    }

    private static Func<IComponentContext, DrawerSettings> BuildDrawerSettings(Options options) =>
        c => new DrawerSettings(
            c.Resolve<IColorAlgorithm>(),
            new Size(options.Size, options.Size),
            options.Font);

    private static Func<IComponentContext, ICloudForm> BuildCloudForm(Options options) =>
        _ => options.Algorithm.ToLower().Equals("circular")
            ? new CircularSpiral(new Point(options.Size / 2, options.Size / 2))
            : new SquareSpiral(new Point(options.Size / 2, options.Size / 2));
}