using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto).NotNull();
        RuleFor(x => x.OrderDto.Id).NotEqual(Guid.Empty).WithMessage("No order to update is specified");
        RuleFor(c => c.OrderDto.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(c => c.OrderDto.CustomerId).NotEmpty().WithMessage("Customer is required");
        RuleFor(c => c.OrderDto.OrderItems).NotEmpty().WithMessage("Orders need to have at least one item");
    }
}