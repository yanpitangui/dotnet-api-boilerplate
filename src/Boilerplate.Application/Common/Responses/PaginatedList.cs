using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Application.Common.Responses;

public record PaginatedList<T>
{
    public PaginatedList()
    {
    }
    
    public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
    {
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalItems = count;
        Result.AddRange(items);
    }

    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalItems { get; init; }

    public List<T> Result { get; init; } = [];
}

public static class PaginatedListHelper
{
    public const int DefaultPageSize = 15;
    public const int DefaultCurrentPage = 1;

    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int currentPage,
        int pageSize)
    {
        currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
        pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
        int count = await source.CountAsync();
        List<T> items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, currentPage, pageSize);
    }
}