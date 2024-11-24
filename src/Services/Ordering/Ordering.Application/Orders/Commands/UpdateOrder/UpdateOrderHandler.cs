namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.OrderDto.Id);
        var existingOrder = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (existingOrder is null)
        {
            throw new OrderNotFoundException(request.OrderDto.Id);
        }

        UpdateExistingOrder(existingOrder, request.OrderDto);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    private static void UpdateExistingOrder(Order existingOrder, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);

        var updatedBillingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);

        var updatedPayment = Payment.Of(
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.ExpirationDate,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod);

        existingOrder.Update(OrderName.Of(orderDto.OrderName), updatedBillingAddress, updatedShippingAddress,
            updatedPayment, OrderStatus.Pending);
    }
}