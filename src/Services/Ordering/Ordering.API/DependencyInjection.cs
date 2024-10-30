namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //TODO: Register services
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        //TODO: Register services
        return app;
    }
}