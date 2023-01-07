using MediatR;

namespace ConsoleApp.RequestResponse;

public class OneWayAsyncHandler : AsyncRequestHandler<OneWayAsync>
{
    protected override Task Handle(OneWayAsync request, CancellationToken cancellationToken)
    {
        Console.WriteLine("In Async One Way Handler");
        return Task.CompletedTask;
    }
}