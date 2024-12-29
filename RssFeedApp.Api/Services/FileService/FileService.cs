using Microsoft.Extensions.Options;
using Exception = System.Exception;
using Options = RssFeedApp.Api.Models.Options;

namespace RssFeedApp.Api.Services.FileService;

public class FileService(
    IOptions<Options> options,
    ILogger<FileService> logger)
    : IFileService
{
    public async Task<string> ReadFileAsync()
    {
        try
        {
            await using var fileStream = new FileStream(
                options.Value.FeedsFile, 
                FileMode.Open, 
                FileAccess.Read, 
                FileShare.None);
            
            using var reader = new StreamReader(fileStream);
            return await reader.ReadToEndAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while reading the file.");
            return string.Empty;
        }
    }

    public async Task WriteFileAsync(string content)
    {
        try
        {
            await using var fileStream = new FileStream(
                options.Value.FeedsFile,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            await using var writer = new StreamWriter(fileStream);
            await writer.WriteAsync(content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while writing to the file.");
        }
    }
}