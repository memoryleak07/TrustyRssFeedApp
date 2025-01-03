using System.Text.Json;
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
    public Task<Pagination<RssFeed>> Search(string? query, int pageIndex = 0, int pageSize = 10)
    {
        var feeds = GetFeedsFromFile();
        
        var filteredFeeds = string.IsNullOrEmpty(query)
            ? feeds
            : feeds.Where(feed => feed.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                                  feed.Description.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();

        return Task.FromResult(filteredFeeds.ToPagination(pageSize, pageIndex));
    }
    
    private ICollection<RssFeed> GetFeedsFromFile()
    {
        var json = fileService.ReadFileAsync().Result;
        if (string.IsNullOrEmpty(json))
        {
            logger.LogError("Feeds file is empty.");
            return new List<RssFeed>();
        }
        var feeds = JsonSerializer.Deserialize<ICollection<RssFeed>>(json);
        return feeds ?? new List<RssFeed>();
    }
}
