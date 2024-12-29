namespace RssFeedApp.Api.Models.Base;

public class ResponseBase<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }

    public ResponseBase() { }

    public ResponseBase(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }
}