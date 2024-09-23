
using BuildingBlocks.CQRS;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal? Price) : ICommand<UpdateProductResult>;
public record UpdateProductResponse(bool IsSuccesfull);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", (UpdateProductRequest request, ISender sender) => HandleRequest(request, sender))
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Update Product")
            .WithSummary("Update Product");
    }

    private async Task<IResult> HandleRequest(UpdateProductRequest request, ISender sender)
    {
        var command = request.Adapt<UpdateProductCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<UpdateProductResponse>();
        return Results.Ok(response);
    }
}
