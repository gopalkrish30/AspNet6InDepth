using Api.middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IMiddleware,FactoryMiddleware>();

builder.Services.AddCors( options =>
{
    options.AddPolicy("Dev_Policy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
    options.AddPolicy("Prod_Policy", builder =>
    {
        builder.WithOrigins("https://www.google.com");
        builder.WithMethods("GET", "POST");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Inline Middleware

//app.Map is used to map the route to the middleware
app.Map("/test-middleware", (app) =>
{
    app.Run( async (context) =>
    {
        await context.Response.WriteAsync("Map Middleware is executed");
    });
});

app.MapPost("/test-post-middleware", async (context) =>
{
    await context.Response.WriteAsync("Map Middleware called from post call is executed");
});


//app.Use gives control to the next middleware.

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("Entering First Middleware"); 
await next();
app.Logger.LogInformation("Exiting First Middleware");
});

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("Entering Second Middleware");
    await next.Invoke();
    app.Logger.LogInformation("Exiting Second Middleware");
});

if (app.Environment.IsDevelopment())
    app.UseCors("Dev_Policy");
else
    app.UseCors("Prod_Policy");

    //app.UseMiddleware<CustomMiddleware>();

    //app.UseMiddleware<IMiddleware>();

app.UseMiddleware<CorreleationIdMiddleware>();

app.UseMiddleware<UnhandledExceptionMiddleware>();

////app.Run terminates the flow
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello world");
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
