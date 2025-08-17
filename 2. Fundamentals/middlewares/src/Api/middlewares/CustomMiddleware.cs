using System.Threading.Tasks;

namespace Api.middlewares
{
    public class CustomMiddleware
    {
        // Dependency Inject the Request Delegate in the constructor
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        //Implement the invoke method
        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Entering the Custom Middleware");
            await _next.Invoke(context);
            _logger.LogInformation("Exiting the custom Middleare");
        }
    }
}
