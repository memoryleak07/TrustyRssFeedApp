using RssFeedApp.Domain.Models;

namespace RssFeedApp.Api.Services.SourceService;

public interface ISourceService
{
    Task<ICollection<Source>> GetSources();
}