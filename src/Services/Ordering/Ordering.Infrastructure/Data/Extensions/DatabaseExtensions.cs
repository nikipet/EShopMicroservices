using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        
        await SeedAsync(dbContext);
    }

    private static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedCustomersAsync(dbContext);
        await SeedProductsAsync(dbContext);
        await SeedOrdersWithItemsAsync(dbContext);
    }

    private static async Task SeedCustomersAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Customers.Any())
        {
            await dbContext.Customers.AddRangeAsync(InitialData.Customers);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Products.Any())
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedOrdersWithItemsAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Orders.Any())
        {
            await dbContext.Orders.AddRangeAsync(InitialData.Orders);
            await dbContext.SaveChangesAsync();
        }
    }
}