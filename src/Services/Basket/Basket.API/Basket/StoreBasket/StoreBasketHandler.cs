using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandValidation : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidation()
    {
        RuleFor(c => c.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(c => c.Cart.Username).NotEmpty().WithMessage("Username cannot be empty");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository basketRepository,
    DiscountService.DiscountServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command, cancellationToken);

        var result = await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(result.Username);
    }

    private async Task DeductDiscount(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
            var discount = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= discount.Amount;
        }
    }
}