using MediatR;

using Microsoft.Extensions.Logging;

using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[Start] Handle Request = {typeof(TRequest).Name} - Response={typeof(TResponse).Name} with request data = {request}");

        var sw = new Stopwatch();
        sw.Start();

        var response = await next();

        sw.Stop();

        if (sw.Elapsed.Seconds > 3)
        {
            _logger.LogWarning($"[Performance] Request {typeof(TRequest).Name} took {sw.Elapsed.Seconds}");
        }

        _logger.LogInformation($"[END] Handled Request = {typeof(TRequest).Name} - Response = {typeof(TResponse).Name}");

        return response;

    }
}
