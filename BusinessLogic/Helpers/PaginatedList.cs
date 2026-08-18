using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;


namespace BusinessLogic.Helpers;

public class PaginatedList<T> 
{
    private PaginatedList(
        List<T> items,
        int count,
        int pageNumber,
        int pageSize)
    {
        Items = items;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
    }

    public IReadOnlyList<T> Items { get; }

    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        int maxPageSize)
    {
        if (source == null)
            throw new HttpException("Source cannot be null.", HttpStatusCode.BadRequest);

        if (pageNumber < 1)
            throw new HttpException("Page number must be greater than 0.", HttpStatusCode.BadRequest);

        if (pageSize < 1)
            throw new HttpException("Page size must be greater than 0.", HttpStatusCode.BadRequest);

        if (pageSize > maxPageSize)
            throw new HttpException($"Page size cannot exceed {maxPageSize}.", HttpStatusCode.BadRequest);
        
        var count = await source.CountAsync();

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}