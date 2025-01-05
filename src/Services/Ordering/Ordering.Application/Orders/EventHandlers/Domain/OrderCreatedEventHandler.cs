using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint,
    FeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent eventData, CancellationToken cancellationToken)
    {
        logger.LogInformation($"OrderCreatedEvent  for order with ID: {eventData.Order.Id.Value} has been handled");
        
        if (await featureManager.IsEnabledAsync("OrderFulfillment", cancellationToken))
        {
            var orderList = new List<Order> { eventData.Order }.ToOrderDtoList();
            await publishEndpoint.Publish(orderList.Single(), cancellationToken);
        }
    }
}