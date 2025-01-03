using RssFeedApp.Api.Services.SourceService;

namespace RssFeedApp.Api.Endpoints;

public static class SourceEndpoints
{
    public static void MapSourceEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var api = endpoints.MapGroup("/api/sources");
        
        api.MapGet("/all", async (ISourceService sourceService) => await sourceService.GetSources())
            .Produces<string[]>()
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Get all sources")
            .WithOpenApi();
    }
}