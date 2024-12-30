namespace RssFeedApp.Api.Models.Base;

public class ResultBase<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public ResultBase(T data)
    {
        Success = true;
        Data = data;
    }
    
    public ResultBase(Exception exception)
    {
        Success = false;
        Message = exception.Message;
    }
}