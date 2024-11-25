using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Domain.Models;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<Order> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .WithDescription("Returns a list of orders for a customer")
            .WithSummary("Returns a list of orders for a customer")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}