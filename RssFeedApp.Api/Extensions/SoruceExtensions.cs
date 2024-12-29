using System.ServiceModel.Syndication;
using RssFeedApp.Api.Models;

namespace RssFeedApp.Api.Extensions;

public static class SourceExtensions
{
    public static ICollection<Source> ToSources(this SyndicationFeed? feed)
    {
        if (feed == null) return null!;
        
        return new List<Source>
        {
            new Source
            {
                Name = feed.Title?.Text ?? string.Empty,
                Url = feed.Links.FirstOrDefault()?.Uri.ToString() ?? string.Empty,
                Tag = feed.Categories.FirstOrDefault()?.Name ?? string.Empty
            }
        };
    }
} 