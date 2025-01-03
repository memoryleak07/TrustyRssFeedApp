using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using RssFeedApp.Api.Extensions;
using RssFeedApp.Api.Services.FileService;
using RssFeedApp.Domain.Base;
using RssFeedApp.Domain.Models;
using Options = RssFeedApp.Domain.Models.Options;

namespace RssFeedApp.Api.Services.FeedService;

public class FeedService(
    IFileService fileService,
    IOptions<Options> options,
    ILogger<FeedService> logger) 
    : IFeedService
{
    // public Task<ICollection<RssFeed>> GetAll() => Task.FromResult(GetFeedsFromFile());
    //
    // public Task<ICollection<RssFeed>> GetLast24() => Task.FromResult<ICollection<RssFeed>>(GetFeedsFromFile()
    //     .Select(feed => new RssFeed
    //     {
    //         Tag = feed.Tag,
    //         Title = feed.Title,
    //         Link = feed.Link,
    //         Description = feed.Description,
    //         Items = feed.Items.Where(item => item?.PublishDate >= DateTime.Now.AddDays(-1)).ToList()
    //     })
    //     .ToList());
    //
    // public Task<ICollection<RssFeed>> GetByTag(string tag) => Task.FromResult<ICollection<RssFeed>>(GetFeedsFromFile()
    //     .Where(feed => string.Equals(feed.Tag, tag, StringComparison.CurrentCultureIgnoreCase))
    //     .ToList());
    //
    public async Task<Results<Ok<Pagination<RssFeed>>, NotFound>> Search(string? query, int pageIndex = 0, int pageSize = 10)
    {
        var feeds = await GetFeedsFromFileAsync();
        
        var filteredFeeds = string.IsNullOrEmpty(query)
            ? feeds
            : feeds.Where(feed => feed.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                                  feed.Description.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        
        if (filteredFeeds.Count == 0)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(filteredFeeds.ToPagination(pageSize, pageIndex));
    }
    
    private async Task<ICollection<RssFeed>> GetFeedsFromFileAsync()
    {
        var json = await fileService.ReadFileAsync();
        if (string.IsNullOrEmpty(json))
        {
            logger.LogError("Feeds file is empty.");
            return new List<RssFeed>();
        }
        var feeds = JsonSerializer.Deserialize<ICollection<RssFeed>>(json);
        return feeds ?? new List<RssFeed>();
    }
}
