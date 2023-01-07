using MediatR;
using ClassLibrary;
using ConsoleApp.Notifications;
using ConsoleApp.RequestResponse;

namespace ConsoleApp;

public class MediatorService : IMediatorService
{
    private readonly IMediator _mediator;

    public MediatorService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Notify(string notifyText)
    {
        _mediator.Publish(new NotificationMessage { Message = notifyText });
    }

    public void OneWay()
    {
        Task.Run(async () => await _mediator.Send(new OneWayAsync()));
        _mediator.Send(new OneWaySync());
    }

    public string RequestResponse()
    {
        string response = Task.Run(
            async () => await _mediator.Send(new Ping())
        ).Result;
        return response;
    }

    public string RequestResponseOtherLib()
    {
        string response = Task.Run(
            async () => await _mediator.Send(new List.Query())
        ).Result;
        return response;
    }
}
