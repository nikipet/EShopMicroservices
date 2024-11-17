using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // The commented out code is what was shown in the video. However, typeof().Assembly is supposed to be faster
        // Or at least this is the case according to this StackOverflow thread:
        // https://stackoverflow.com/questions/15407340/difference-between-assembly-getexecutingassembly-and-typeofprogram-assembly
        // Found additional info on the issue => https://rules.sonarsource.com/csharp/tag/performance/RSPEC-3902/
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}