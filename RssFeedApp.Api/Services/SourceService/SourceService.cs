using Microsoft.Extensions.Options;
using RssFeedApp.Domain.Models;
using Options = RssFeedApp.Domain.Models.Options;

namespace RssFeedApp.Api.Services.SourceService;

public class SourceService(IOptions<Options> options) : ISourceService
{
    public async Task<ICollection<Source>> GetSources() => await Task.FromResult(options.Value.Sources);
}