using System.Text.Json.Serialization;

namespace CompanyTracker.Api.Domain.DTO;

public class ErrorResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";
}
