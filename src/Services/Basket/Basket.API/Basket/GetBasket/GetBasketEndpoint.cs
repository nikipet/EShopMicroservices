


namespace Basket.API.Basket.GetBasket;

// public record GetBasketRequest(string Username);
public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
        {
            // TODO: Change this to use the Request once it is uncommented
            var query = new GetBasketQuery(username);
            var result = await sender.Send(query);
            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        }).Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get Basket for a user")
        .WithSummary("Get Basket for a user");
    }
}
