using RssFeedApp.Api.Models;

namespace RssFeedApp.Api.Services.FeedService;

public interface IFeedService
{
    Task<ICollection<RssFeed>> GetAll();
    Task<ICollection<RssFeed>> GetLast24();
    Task<ICollection<RssFeed>> GetByTag(string tag);
}