namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccessful);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", (Guid id, ISender sender) => HandleRequest(id, sender))
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithDescription("Delete a product")
            .WithSummary("Delete a product");
    }

    private async Task<IResult> HandleRequest(Guid id , ISender sender)
    {
        var result = await sender.Send(new DeleteProductCommand(id));
        var response = result.Adapt<DeleteProductResponse>();

        return Results.Ok(response);
    }
}
