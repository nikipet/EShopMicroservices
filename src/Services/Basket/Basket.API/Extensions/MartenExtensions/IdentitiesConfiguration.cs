using Marten;

namespace Basket.API.Extensions.MartenExtensions;

public static class IdentitiesConfiguration
{
    public static void OverrideSchemaIdentities(this StoreOptions opts)
    {
        opts.Schema.For<ShoppingCart>().Identity(s => s.Username);
    }
}
