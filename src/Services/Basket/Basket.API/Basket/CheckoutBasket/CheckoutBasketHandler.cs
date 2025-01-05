using Basket.API.Data;
using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(command => command.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null");
        RuleFor(command => command.BasketCheckoutDto.Username).NotEmpty()
            .WithMessage("BasketCheckoutDto Username cannot be empty");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasketAsync(command.BasketCheckoutDto.Username, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.Total;
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasketAsync(command.BasketCheckoutDto.Username, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}