using Microsoft.Extensions.Logging;

namespace ConsoleApp;

public class App
{
    private readonly ILogger<App> _logger;
    private readonly IMediatorService _notifierMediatorService;
    public App(
        ILogger<App> logger,
        IMediatorService notifierMediatorService)
    {
        _logger = logger;
        _notifierMediatorService = notifierMediatorService;
    }

    public void Run()
    {
        _logger.LogInformation("Run started");
        Notify();
        RequestResonse();
        OneWay();
        RequestResponseOtherLib(); // Notice that the IRequest and IRequestHandler implementations for this one reside in an external assembly

        _logger.LogInformation("Run completed");
    }

    private void Notify()
    {
        _notifierMediatorService.Notify("Test Message");
    }

    private void RequestResonse()
    {
        string response = _notifierMediatorService.RequestResponse();
        Console.WriteLine($"In App: {response}");
    }

    private void OneWay()
    {
        _notifierMediatorService.OneWay();
    }

    private void RequestResponseOtherLib()
    {
        string response = _notifierMediatorService.RequestResponseOtherLib();
        Console.WriteLine($"In App: {response} (from external class library)");
    }
}
