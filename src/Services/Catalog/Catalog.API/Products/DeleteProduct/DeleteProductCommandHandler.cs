using BuildingBlocks.CQRS;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccessful);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id of the product to be deleted is required");
    }
}
internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async  Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        session.Delete<Product>(request.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
