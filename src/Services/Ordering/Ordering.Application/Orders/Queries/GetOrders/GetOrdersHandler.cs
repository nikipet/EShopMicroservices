using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await
            dbContext.Orders
                .AsNoTracking()
                .OrderBy(o => o.OrderName)
                .Include(o => o.OrderItems)
                .Skip(request.PaginationRequest.PageNumber * request.PaginationRequest.PageSize)
                .Take(request.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(request.PaginationRequest.PageNumber,
                request.PaginationRequest.PageSize,
                orders.Count,
                orders.ToOrderDtoList()));
    }
}