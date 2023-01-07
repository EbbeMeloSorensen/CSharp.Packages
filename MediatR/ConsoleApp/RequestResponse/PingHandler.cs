using MediatR;

namespace ConsoleApp.RequestResponse;

/// <summary>
/// Asycnhronous Handle for Request & response
/// </summary>
public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Pong");
    }
}