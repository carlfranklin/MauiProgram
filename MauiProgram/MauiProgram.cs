using Microsoft.Extensions.Logging;

namespace MauiProgram;

public static class MauiProgram
{
    // This method returns the entry point (a MauiApp) and is called
    // from each platform's entry point.
    public static MauiApp CreateMauiApp()
    {
        // MauiApp.CreateBuilder returns a MauiAppBuilder, which
        // is used to configure fonts, resources, and services.
        var builder = MauiApp.CreateBuilder();

        builder
            // We give it our main App class, which derives from Application.
            // App is defined in App.xaml.cs.
            .UseMauiApp<App>()
            // Default font configuration.
            .ConfigureFonts(fonts =>
            {
                // AddFont takes a required filename (first parameter)
                // and an optional alias for each font.
                // When using these fonts in XAML you can use them
                // either by filename (without the extension,) or the alias.
                fonts.AddFont("Consolas-Regular.ttf", "ConsolasRegular");
				fonts.AddFont("Consolas-Bold.ttf", "ConsolasBold");
				fonts.AddFont("Consolas-Italic.ttf", "ConsolasItalic");
				fonts.AddFont("Consolas-Bold-Italic.ttf", "ConsolasBoldItalic");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Register services
            .Services
                .AddSingleton<MainPage>()
                .AddSingleton<IApiService, ApiService>()
                // Add Logging
                .AddLogging(configure =>
                {
                    configure.AddDebug();
                });

        // The MauiAppBuilder returns a MauiApp application.
        return builder.Build();
    }
}