namespace MauiProgram;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Consolas-Regular.ttf", "ConsolasRegular");
				fonts.AddFont("Consola-Bold.ttf", "ConsolasBold");
				fonts.AddFont("Consolas-Italic.ttf", "ConsolasItalic");
				fonts.AddFont("Consolas-Bold-Italic.ttf", "ConsolasBoldItalic");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
