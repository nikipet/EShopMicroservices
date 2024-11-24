using FluentValidation;
using Ordering.Application.Data;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    CreateOrderCommandValidator()
    {
        RuleFor(c=>c.OrderDto.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(c=>c.OrderDto.CustomerId).NotEmpty().WithMessage("Customer is required");
        RuleFor(c=>c.OrderDto.OrderItems).NotEmpty().WithMessage("Orders need to have at least one item");
    }
}