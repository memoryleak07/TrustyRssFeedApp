using System.ServiceModel.Syndication;
using System.Text.Json;
using System.Xml;
using Microsoft.Extensions.Options;
using RssFeedApp.Api.Extensions;
using RssFeedApp.Api.Services.FileService;
using RssFeedApp.Api.Services.SourceService;
using RssFeedApp.Domain.Models;
using Options = RssFeedApp.Domain.Models.Options;

namespace RssFeedApp.Api.Services.PullTimedService;

public class PullTimedService(
    IFileService fileService,
    ISourceService sourceService,
    ILogger<PullTimedService> logger, 
    IOptions<Options> options) 
    : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("PullTimedService running. Interval is {Interval} hours.",
            options.Value.PullIntervalHours);
        
        // _timer = new Timer(DoWork, null, TimeSpan.Zero,
        //     TimeSpan.FromHours(options.Value.PullIntervalHours));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        logger.LogInformation("PullTimedService start job.");
        
        var sources = sourceService.GetSources().Result;
        var feeds = new List<RssFeed>();
        try
        {
            foreach (var source in sources)
            {
                using var reader = XmlReader.Create(source.Url);
                var feed = SyndicationFeed.Load(reader);
                var rssFeed = feed.ToRssFeed(source.Tag);
                if (rssFeed == null) continue;
                feeds.Add(rssFeed);
            }
            
            var json = JsonSerializer.Serialize(feeds, _jsonSerializerOptions);
            if (string.IsNullOrEmpty(json))
            {
                logger.LogError("No feeds to write.");
                return;
            }
            
            fileService.WriteFileAsync(json).Wait();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while pulling feeds.");
        }
        finally
        {
            logger.LogInformation("PullTimedService job done.");
            logger.LogInformation("Feeds pulled: {Feeds}", feeds.Count);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("PullTimedService is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}