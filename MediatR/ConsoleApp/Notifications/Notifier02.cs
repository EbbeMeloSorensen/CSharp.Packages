using MediatR;

namespace ConsoleApp.Notifications;

public class Notifier02 : INotificationHandler<NotificationMessage>
{
    public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notifier 02 -> Message: {notification.Message}");
        return Task.CompletedTask;
    }
}