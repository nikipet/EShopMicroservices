namespace BuildingBlocks.Pagination;

public record PaginationRequest(int PageNumber = 0, int PageSize = 20);