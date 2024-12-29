using System.ServiceModel.Syndication;
using RssFeedApp.Api.Models;

namespace RssFeedApp.Api.Extensions;

public static class RssFeedExtensions
{
    public static RssFeed? ToRssFeed(this SyndicationFeed? feed, string? tag)
    {
        if (feed == null) return null;
        
        return new RssFeed
        {
            Tag = tag ?? string.Empty,
            Title = feed.Title?.Text ?? string.Empty,
            Description = feed.Description?.Text ?? string.Empty,
            Link = feed.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
            Items = feed.Items?.Select(item => item.ToRssFeedItem()).ToList() ?? []
        };
    }
    
    private static RssFeedItem? ToRssFeedItem(this SyndicationItem? item)
    {
        if (item == null) return null;

        return new RssFeedItem
        {
            Title = item.Title?.Text ?? string.Empty,
            Summary = item.Summary?.Text ?? string.Empty,
            PublishDate = item.PublishDate.UtcDateTime,
            Link = item.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
            Content = item.Content?.ToString() ?? string.Empty
        };
    }
} 