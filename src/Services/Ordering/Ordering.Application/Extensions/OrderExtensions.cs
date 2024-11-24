namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order>? orders)
    {
        if (orders == null)
        {
            return [];
        }

        var orderDtos = new List<OrderDto>();

        foreach (var order in orders)
        {
            var shippingAddressDto = new AddressDto(
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress,
                order.ShippingAddress.AddressLine,
                order.ShippingAddress.Country,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode
            );

            var billingAddressDto = new AddressDto(
                order.BillingAddress.FirstName,
                order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress,
                order.BillingAddress.AddressLine,
                order.BillingAddress.Country,
                order.BillingAddress.State,
                order.BillingAddress.ZipCode
            );

            var paymentDto = new PaymentDto(
                order.Payment.CardName,
                order.Payment.CardNumber,
                order.Payment.ExpirationDate,
                order.Payment.Cvv,
                order.Payment.PaymentMethod
            );

            var orderItemsDto = order.OrderItems
                .Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList();

            var dto = new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                shippingAddressDto,
                billingAddressDto,
                paymentDto,
                order.Status,
                orderItemsDto
            );
            orderDtos.Add(dto);
        }

        return orderDtos;
    }
}