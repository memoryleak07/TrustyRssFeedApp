using System.Text.Json;

namespace RssFeedApp.Api.Models.Base;

// public static class ErrorRespondCode
// {
//     public const string NOT_FOUND = "not_found";
//     public const string VERSION_CONFLICT = "version_conflict";
//     public const string ITEM_ALREADY_EXISTS = "item_exists";
//     public const string CONFLICT = "conflict";
//     public const string BAD_REQUEST = "bad_request";
//     public const string UNAUTHORIZED = "unauthorized";
//     public const string INTERNAL_ERROR = "internal_error";
//     public const string GENERAL_ERROR = "general_error";
//     public const string UNPROCESSABLE_ENTITY = "unprocessable_entity";
// }

public static class ErrorMessage
{
    public const string InternalError = "Something went wrong. Please try again later.";
    public const string NotFoundMessage = "The requested resource could not be found.";
    public const string AppConfigurationMessage = "Unable to retrieve application settings.";
    public const string TransactionNotCommit = "The transaction could not be committed.";
    public const string TransactionNotExecute = "The transaction could not be executed.";
}

public record Error(string? Code, string Message, Guid ErrorId)
{
    public static implicit operator string(Error error) => JsonSerializer.Serialize(error);
};