namespace Bunny.API.NET.Entities;

/// <summary>
///     Represents the result of an operation, including error information, status code, and success status.
/// </summary>
public class Result
{
    /// <summary>
    ///     Gets or sets the error information associated with the result.
    /// </summary>
    public ResultError? Error { get; set; }
    /// <summary>
    ///     Gets or sets the HTTP status code of the result.
    /// </summary>
    public HttpStatusCode? StatusCode { get; set; }
    /// <summary>
    ///     Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }
}
/// <summary>
///     Represents the result of an operation with a specific data type.
/// </summary>
/// <typeparam name="T">The type of the data associated with the result.</typeparam>
public class Result<T> : Result
{
    /// <summary>
    ///     Gets or sets the data associated with the result.
    /// </summary>
    public T? Data { get; set; }
}
/// <summary>
///     Represents error information associated with a result.
/// </summary>
public class ResultError
{
    /// <summary>
    ///     Gets or sets the key identifying the error.
    /// </summary>
    public string? ErrorKey { get; set; }
    /// <summary>
    ///     Gets or sets the field associated with the error.
    /// </summary>
    public string? Field { get; set; }
    /// <summary>
    ///     Gets or sets the error message.
    /// </summary>
    public string? Message { get; set; }
}
