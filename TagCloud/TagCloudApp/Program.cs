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
                Console.WriteLine($"Путь к файлу: {options.FilePath}");
                Console.WriteLine($"Алгоритм: {options.Algorithm}");
                Console.WriteLine($"Алгоритм расцветки слов: {options.WordsColor}");
                Console.WriteLine($"Шрифт: {options.Font}");
                Console.WriteLine($"Размер: {options.Size}");
                var container = ConfigureContainer(options);
                container.Resolve<ITextHandler>();
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
        builder.RegisterType<TextHandler>().As<ITextHandler>();
        RegisterColoringAlgorithm(builder, options);
        RegisterDrawerSettings(builder, options);
        RegisterCLoudAlgorithm(builder, options);
        builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<RectanglesGenerator>().As<IRectanglesGenerator>();
        builder.RegisterType<RectanglesCloudDrawer>().As<ICloudDrawer>();
        return builder.Build();
    }

    private static void RegisterColoringAlgorithm(ContainerBuilder builder, Options options)
    {
        if (options.WordsColor.ToLower().Equals("random"))
        {
            builder.RegisterType<RandomColor>().As<IColorAlgorithm>();
        }
        else if (Color.FromName(options.WordsColor).IsKnownColor)
        {
            builder.RegisterType<SingleColor>().As<IColorAlgorithm>()
                .WithParameter(new TypedParameter(typeof(Color), Color.FromName(options.WordsColor)));
        }
        else
        {
            var gradient = options.WordsColor.Split("-").Select(Color.FromName).ToArray();
            builder.RegisterType<GradientColor>().As<IColorAlgorithm>()
                .WithParameter(new TypedParameter(typeof(Color[]), gradient));
        }
    }

    private static void RegisterDrawerSettings(ContainerBuilder builder, Options options)
    {
        var registerType = builder.RegisterType<DrawerSettings>();
        registerType.WithParameter(new TypedParameter(typeof(Size), new Size(options.Size, options.Size)));
        registerType.WithParameter(new TypedParameter(typeof(string), options.Font));
    }

    private static void RegisterCLoudAlgorithm(ContainerBuilder builder, Options options)
    {
        if (options.Algorithm.Equals("circular"))
        {
            builder.RegisterType<CircularSpiral>().As<ICloudForm>()
                .WithParameter(new TypedParameter(typeof(Point), new Point(options.Size / 2)));
        }
        else
        {
            builder.RegisterType<SquareSpiral>().As<ICloudForm>()
                .WithParameter(new TypedParameter(typeof(Point), new Point(options.Size / 2)));
        }
    }
}