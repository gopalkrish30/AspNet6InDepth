
namespace Api.middlewares
{
    public class FactoryMiddleware : IMiddleware
    {
        private readonly ILogger<FactoryMiddleware> _logger;

        public FactoryMiddleware(ILogger<FactoryMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Entering Factory Middleware");
            await context.Response.WriteAsync("From Factory Middelware");
            _logger.LogInformation("Exiting Factory Middleware");
        }
    }
}
