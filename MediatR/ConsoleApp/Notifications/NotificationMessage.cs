using MediatR;

namespace ConsoleApp.Notifications;

public class NotificationMessage : INotification
{
    public string Message { get; set; }
}