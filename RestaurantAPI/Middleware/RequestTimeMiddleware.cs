using System.Diagnostics;

namespace RestaurantAPI.Middleware;

public class RequestTimeMiddleware:IMiddleware 
{
    private readonly Stopwatch _stopWatch;
    private readonly ILogger<RequestTimeMiddleware> _logger;

    public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
    {
        _logger = logger;
        _stopWatch = new Stopwatch();
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopWatch.Start();
        await next.Invoke(context);
        _stopWatch.Stop();

        var elapsedMillisenconds = _stopWatch.ElapsedMilliseconds;
        if (elapsedMillisenconds / 1000 > 4)
        {
            var message =
                $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMillisenconds} ms";
            _logger.LogInformation(message);
        }
    }
}