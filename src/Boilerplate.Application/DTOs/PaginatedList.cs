using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Application.DTOs
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; }
        public int TotalPages { get; }

        public PaginatedList(List<T> items, int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }
    }

    public static class PaginatedListHelper 
    {

        public const int DefaultPageSize = 15;
        public const int DefaultCurrentPage = 1;

        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {
            currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
            pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
            var count = await source.CountAsync();
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, currentPage, pageSize);
        }
    }

    

    
}
