using RssFeedApp.Api.Services.FeedService;
using RssFeedApp.Domain.Base;
using RssFeedApp.Domain.Models;

namespace RssFeedApp.Api.Endpoints;

public static class FeedEndpoints
{
    public static void MapFeedEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var api = endpoints.MapGroup("/api/feeds");

        api.MapGet("/search", async (IFeedService feedService, string? query, int pageIndex = 0, int pageSize = 10)
                => await feedService.Search(query, pageIndex, pageSize))
            .Produces<Pagination<RssFeed>>()
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Search feeds")
            .WithOpenApi();
    }
}