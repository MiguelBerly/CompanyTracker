using System.Text.Json.Serialization;

namespace CompanyTracker.Domain.DTO;

public class CompanyWithApplications
{
    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("applied")]
    public bool Applied { get; set; }
}

