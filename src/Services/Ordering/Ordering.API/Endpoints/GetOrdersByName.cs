using Ordering.Application.Orders.Queries.GetOrderByName;
using Ordering.Domain.Models;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameRequest(string OrderName);

public record GetOrdersByNameResponse(IEnumerable<Order> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));
                var response = result.Adapt<GetOrdersByNameResponse>();
                Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .WithDescription("Returns a list of orders")
            .WithSummary("Returns a list of orders")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}