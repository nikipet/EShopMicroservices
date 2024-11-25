using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto OrderDto);

public record CreateOrderResponse(Guid OrderId);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var createOrderCommand = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(createOrderCommand);

                var response = result.Adapt<CreateOrderResponse>();
                Results.Created($"orders/{response.OrderId}", response);
            })
            .WithName("CreateOrder")
            .WithDescription("Creates a new order")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new order");
    }
}