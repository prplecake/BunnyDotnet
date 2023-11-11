namespace Bunny.NET.Entities;

public class Result
{
    public ResultError? Error { get; set; }
    public HttpStatusCode? StatusCode { get; set; }
    public bool Success { get; set; }
}
public class ResultError
{
    public string? ErrorKey { get; set; }
    public string? Field { get; set; }
    public string? Message { get; set; }
}
