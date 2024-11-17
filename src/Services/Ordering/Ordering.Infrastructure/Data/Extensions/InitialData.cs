using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    private static readonly Guid PhoneXId = new Guid("84999ce9-9a63-45dc-94fd-4abda9ef3900");
    private static readonly Guid SamsungId = new Guid("33404974-7880-4c4b-bd95-b150e7b66c67");
    private static readonly Guid MehmetId = new Guid("5a60ccd9-8fd4-42d9-8e5b-fb0e5cb1614d");
    private static readonly Guid IvanId = new Guid("5809b3fb-80c8-4aed-921d-98067579c657");

    private static readonly Guid OrderOneId = new Guid("1c2b0b3a-041c-4323-afb7-ad3c12b47aac");
    private static readonly Guid OrderTwoId = new Guid("b1684dfc-c1a4-42fe-a395-66cc4d845335");

    internal static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(MehmetId), "Mehmet", "Mehmet@test.com"),
        Customer.Create(CustomerId.Of(IvanId), "Ivan", "ivan@test.com")
    ];

    internal static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(PhoneXId), "IPhone X", 3000m),
        Product.Create(ProductId.Of(SamsungId), "Samsung Fold 5", 2500m),
    ];

    internal static IEnumerable<Order> Orders
    {
        get
        {
            var addressOne = Address.Of("Ivan", "Ivanov", "Ivanov@test.com", "Sofia Mladost 1", "Bulgaria", "N/A",
                "1750");
            var addressTwo = Address.Of("Georgi", "Georgiev", "Georgi@test.com", "Sofia Center", "Bulgaria", "N/A",
                "1000");

            var paymentOne = Payment.Of("Ivan's Card", "9999-9999-9999-9999", "06/29", "322", 1);
            var paymentTwo = Payment.Of("Georgi's Card", "1111-1111-1111-111", "07/31", "112", 2);

            var orderOne = Order.Create(
                OrderId.Of(OrderOneId),
                CustomerId.Of(MehmetId),
                OrderName.Of("ORD_2"),
                billingAddress: addressOne,
                shippingAddress: addressOne,
                paymentOne);

            orderOne.Add(ProductId.Of(PhoneXId), 2, 3000m);
            orderOne.Add(ProductId.Of(SamsungId), 4, 2500m);


            var orderTwo = Order.Create(
                OrderId.Of(OrderTwoId),
                CustomerId.Of(IvanId),
                OrderName.Of("ORD_2"),
                billingAddress: addressTwo,
                shippingAddress: addressTwo,
                paymentTwo);

            orderTwo.Add(ProductId.Of(PhoneXId), 10, 3000m);
            orderTwo.Add(ProductId.Of(SamsungId), 20, 2500m);
            return [orderOne, orderTwo];
        }
    }
}