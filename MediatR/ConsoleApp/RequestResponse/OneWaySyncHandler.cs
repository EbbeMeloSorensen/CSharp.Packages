using MediatR;

namespace ConsoleApp.RequestResponse;

public class OneWaySyncHandler : RequestHandler<OneWaySync>
{
    protected override void Handle(OneWaySync request)
    {
        Console.WriteLine("In Sync One Way Handler");
    }
}