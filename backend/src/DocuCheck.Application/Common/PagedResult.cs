namespace DocuCheck.Application.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(
        T[] items,
        TotalCount totalCount,
        PageNumber pageNumber,
        PageSize pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    
    public T[] Items { get; init; } = Array.Empty<T>();
    public TotalCount TotalCount { get; init; }
    public PageNumber PageNumber { get; init; }
    public PageSize PageSize { get; init; }
}

public record TotalCount(int Value);

public record PageNumber(int Value);

public record PageSize(int Value);