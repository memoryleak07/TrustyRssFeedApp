using RssFeedApp.Api.Services.SourceService;

namespace RssFeedApp.Api.Endpoints;

public static class SourceEndpoints
{
    public static void MapSourceEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/sources", async (ISourceService sourceService) => await sourceService.GetSources())
            .Produces<string[]>()
            .ProducesValidationProblem()
            .WithDescription("Get all sources")
            .WithOpenApi();
    }
}