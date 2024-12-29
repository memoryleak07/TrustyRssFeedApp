namespace RssFeedApp.Api.Services.FileService;

public interface IFileService
{
    Task<string> ReadFileAsync();
    Task WriteFileAsync(string content);
}