using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.middlewares
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public UnhandledExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                ProblemDetails details;

                if(env.IsDevelopment())
                {
                    details = new ProblemDetails()
                    {
                        Type = ex.GetType().ToString(),
                        Status = 500,
                        Detail = ex.Message
                    };
                }
                else
                {
                    details = new ProblemDetails
                    {
                        Type = ex.GetType().ToString(),
                        Status = 500,
                        Detail = "An Error has Occured"
                    };
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(details));
            }
        }
    }
}
