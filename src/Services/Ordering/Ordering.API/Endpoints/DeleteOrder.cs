using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId:guid}", async (Guid orderId, ISender sender) =>
            {
                var deleteOrderCommand = new DeleteOrderCommand(orderId);
                var result = await sender.Send(deleteOrderCommand);

                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .WithDescription("Delete order")
            .WithSummary("Delete order")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}