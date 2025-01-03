namespace RssFeedApp.Domain.Base;

public class Pagination<T>(int pageSize, int pageIndex, ICollection<T> items)
{
    public int TotalItemsCount { get; set; } = items.Count;
    public int PageSize { get; set; } = pageSize;
    public int PageIndex { get; set; } = pageIndex;
    
    public int TotalPagesCount => (int)Math.Ceiling((double)TotalItemsCount / PageSize);
    public ICollection<T>? Items { get; set; } = items;
}