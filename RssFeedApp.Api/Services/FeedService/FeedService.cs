using System.Text.Json;
using Microsoft.Extensions.Options;
using RssFeedApp.Api.Models;
using RssFeedApp.Api.Services.FileService;
using Options = RssFeedApp.Api.Models.Options;

namespace RssFeedApp.Api.Services.FeedService;

public class FeedService(
    IFileService fileService,
    IOptions<Options> options,
    ILogger<FeedService> logger) 
    : IFeedService
{
    public Task<ICollection<RssFeed>> GetAll() => Task.FromResult(GetFeedsFromFile());

    public Task<ICollection<RssFeed>> GetLast24() => Task.FromResult<ICollection<RssFeed>>(GetFeedsFromFile()
        .Select(feed => new RssFeed
        {
            Tag = feed.Tag,
            Title = feed.Title,
            Link = feed.Link,
            Description = feed.Description,
            Items = feed.Items.Where(item => item.PublishDate >= DateTime.Now.AddDays(-1)).ToList()
        })
        .ToList());
    
    public Task<ICollection<RssFeed>> GetByTag(string tag) => Task.FromResult<ICollection<RssFeed>>(GetFeedsFromFile()
        .Where(feed => string.Equals(feed.Tag, tag, StringComparison.CurrentCultureIgnoreCase))
        .ToList());
    
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