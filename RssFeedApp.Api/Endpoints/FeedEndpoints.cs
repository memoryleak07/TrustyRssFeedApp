using Microsoft.AspNetCore.Http.HttpResults;
using RssFeedApp.Api.Models;
using RssFeedApp.Api.Services.FeedService;

namespace RssFeedApp.Api.Endpoints;

public static class FeedEndpoints
{
    public static void MapFeedEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/all", async (IFeedService feedServices) => await feedServices.GetAll())
            .Produces<Task<Results<Ok<ICollection<RssFeed>>, NotFound>>>()
            .WithDescription("Get all feeds")
            .WithOpenApi();
        
        endpoints
            .MapGet("/last24", async (IFeedService feedServices) => await feedServices.GetLast24())
            .Produces<ICollection<RssFeed>>()
            .WithDescription("Get last 24 hours feeds")
            .WithOpenApi();
        
        endpoints
            .MapGet("/all/{tag}", async (IFeedService feedServices, string tag) => await feedServices.GetByTag(tag))
            .Produces<ICollection<RssFeed>>()
            .WithDescription("Get feeds by tag")
            .WithOpenApi();
    }
}