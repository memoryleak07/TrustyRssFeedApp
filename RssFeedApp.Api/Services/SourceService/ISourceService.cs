using RssFeedApp.Api.Models;

namespace RssFeedApp.Api.Services.SourceService;

public interface ISourceService
{
    Task<ICollection<Source>> GetSources();
}