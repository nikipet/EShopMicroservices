using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation($"Integration Event Handled: {context.Message.GetType().Name}");
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent eventData)
    {
        var shippingAddress = new AddressDto(eventData.FirstName, eventData.LastName, eventData.EmailAddress,
            eventData.AddressLine, eventData.Country, eventData.State, eventData.ZipCode);
        var orderId = Guid.NewGuid();

        var payment = new PaymentDto(eventData.CardName, eventData.CardNumber, eventData.ExpirationDate, eventData.Cvv,
            eventData.PaymentMethod);
        var orderDto = new OrderDto(orderId, eventData.CustomerId, eventData.Username, shippingAddress,
            shippingAddress, payment, OrderStatus.Pending, [
                new OrderItemDto(orderId, new Guid("84999ce9-9a63-45dc-94fd-4abda9ef3900"), 1, 500),
                new OrderItemDto(orderId, new Guid("33404974-7880-4c4b-bd95-b150e7b66c6"), 1, 700)
            ]);
        return new CreateOrderCommand(orderDto);
    }
}