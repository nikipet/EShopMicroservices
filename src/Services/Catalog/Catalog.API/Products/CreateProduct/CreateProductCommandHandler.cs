using BuildingBlocks.CQRS;

using Catalog.API.Products.GetProductById;

using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal? Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c=>c.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(c=>c.Categories).NotEmpty().WithMessage("Products must have at least one category");
        RuleFor(c=>c.ImageFile).NotEmpty().WithMessage("Image file is required");
        RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be larger than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession documentSession)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = new Product
        {
            Name = command.Name,
            Category = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
