using RssFeedApp.Api.Models;
using RssFeedApp.Api.Models.Base;
using RssFeedApp.Api.Services.FeedService;

namespace RssFeedApp.Api.Endpoints;

public static class FeedEndpoints
{
    public static void MapFeedEndpoints(this WebApplication app)
    {
        app.MapGet("/search", async (IFeedService feedServices, string? query, int pageIndex = 0, int pageSize = 10)
            => Results.Ok(new ResultBase<Pagination<RssFeed>>(await feedServices.Search(query, pageIndex, pageSize))))
            .WithDescription("Search feeds")
            .WithOpenApi();
    }
}