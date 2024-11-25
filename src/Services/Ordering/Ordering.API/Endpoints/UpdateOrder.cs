using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto OrderDto);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var updateOrderCommand = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(updateOrderCommand);

                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateOrder")
            .WithDescription("Updates an order")
            .WithSummary("Updates an order")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}