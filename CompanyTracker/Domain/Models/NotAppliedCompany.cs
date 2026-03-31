using System.Text.Json.Serialization;

namespace CompanyTracker.Domain.DTO;

public class NotAppliedCompany
{
    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("contact_person")]
    public string ContactPerson { get; set; } = "";

    [JsonPropertyName("contact_email")]
    public string ContactEmail { get; set; } = "";

    [JsonPropertyName("is_applied")]
    public bool IsApplied { get; set; }
}

