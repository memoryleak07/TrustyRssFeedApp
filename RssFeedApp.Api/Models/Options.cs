namespace RssFeedApp.Api.Models;

public class Options
{
    public string Description { get; init; } = string.Empty;
    public string FeedsFile { get; init; } = string.Empty;
    public ICollection<Source> Sources { get; set; } = [];
    public int PullIntervalHours { get; init; } = 24;
}