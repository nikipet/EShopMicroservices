namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public IEnumerable<TEntity> Data { get; } = data;
    public int Count { get; } = count;
}