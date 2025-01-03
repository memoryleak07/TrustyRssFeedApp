namespace RssFeedApp.Domain.Models;

public class RssFeedItem
{
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string Link { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
}