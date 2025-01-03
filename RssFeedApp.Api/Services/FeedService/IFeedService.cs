﻿using RssFeedApp.Domain.Base;
using RssFeedApp.Domain.Models;

namespace RssFeedApp.Api.Services.FeedService;

public interface IFeedService
{
    // Task<ICollection<RssFeed>> GetAll();
    // Task<ICollection<RssFeed>> GetLast24();
    // Task<ICollection<RssFeed>> GetByTag(string tag);
    Task<Pagination<RssFeed>> Search(string? query, int pageIndex = 0, int pageSize = 10);
}