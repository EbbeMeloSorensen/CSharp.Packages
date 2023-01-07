using MediatR;

namespace ConsoleApp.Notifications;

public class Notifier01 : INotificationHandler<NotificationMessage>
{
    public Task Handle(NotificationMessage notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notifier 01 -> Message: {notification.Message}");
        return Task.CompletedTask;
    }
}