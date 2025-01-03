using Microsoft.AspNetCore.Http.HttpResults;
using RssFeedApp.Domain.Base;
using RssFeedApp.Domain.Models;

namespace RssFeedApp.Api.Services.FeedService;

public interface IFeedService
{
    Task<Results<Ok<Pagination<RssFeed>>, NotFound>> Search(string? query, int pageIndex = 0, int pageSize = 10);
}