using RssFeedApp.Api.Models.Base;

namespace RssFeedApp.Api.Extensions;

public static class PaginationExtensions
{
    public static Pagination<T> ToPagination<T>(this ICollection<T> items, int pageSize, int pageIndex)
        => new Pagination<T>(
            pageSize,
            pageIndex,
            items
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList());
}