using Basket.API.Data;
using Basket.API.Extensions.MartenExtensions;

using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

using Marten;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.OverrideSchemaIdentities();
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(opts => { });
app.Run();
        