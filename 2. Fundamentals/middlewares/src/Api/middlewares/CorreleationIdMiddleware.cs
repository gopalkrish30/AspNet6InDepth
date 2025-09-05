using System.Threading.Tasks;

namespace Api.middlewares
{
    public class CorreleationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string CorrelationHeader = "X-Correleation-Id";
        public CorreleationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string correlationid = Guid.NewGuid().ToString();

            if (!context.Request.Headers.ContainsKey(CorrelationHeader) && !context.Request.Headers.TryGetValue(CorrelationHeader, out _))
            {
                context.Request.Headers.Add(CorrelationHeader, correlationid);
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationHeader, correlationid);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
