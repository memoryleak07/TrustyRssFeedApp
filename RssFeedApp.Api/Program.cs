using RssFeedApp.Api.Endpoints;
using RssFeedApp.Api.Services.FeedService;
using RssFeedApp.Api.Services.FileService;
using RssFeedApp.Api.Services.PullTimedService;
using RssFeedApp.Api.Services.SourceService;
using RssFeedApp.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", config =>
    {
        config.AllowAnyOrigin() 
            .AllowAnyMethod() 
            .AllowAnyHeader();
    });
});

var opt = builder.Configuration
            .AddJsonFile("options.json", optional: false, reloadOnChange: true)
            .Build()
            ?? throw new Exception("File options.json not found.");

builder.Services.Configure<Options>(opt);
builder.Services.AddHostedService<PullTimedService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IFeedService, FeedService>();
builder.Services.AddSingleton<ISourceService, SourceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.MapFeedEndpoints();
app.MapSourceEndpoints();

await app.RunAsync();