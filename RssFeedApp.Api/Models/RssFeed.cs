namespace RssFeedApp.Api.Models;

public class RssFeed
{
    public string Tag { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public List<RssFeedItem?> Items { get; set; } = [];
}

