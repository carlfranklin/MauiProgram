using Microsoft.Extensions.Logging;

namespace MauiProgram;

public partial class MainPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly ILogger<MainPage> _logger;

    public MainPage(IApiService apiService, ILogger<MainPage> logger)
    {
        _apiService = apiService;
        _logger = logger;

        _logger.LogInformation("MainPage constructor called.");
        InitializeComponent();
    }

    private void OnGetDataClicked(object sender, EventArgs e)
    {
        _logger.LogInformation("OnGetDataClicked called.");

        CounterLabel.Text = $"Test Data: {_apiService?.GetTestData()}";
        SemanticScreenReader.Announce(CounterLabel.Text);
    }
}