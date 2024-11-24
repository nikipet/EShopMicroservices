namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrderByNameResult>;
public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);