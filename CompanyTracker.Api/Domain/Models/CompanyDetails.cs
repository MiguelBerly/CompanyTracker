using System.Text.Json.Serialization;

namespace CompanyTracker.Api.Domain.DTO;

public class CompanyDetails
{
    [JsonPropertyName("company_id")]
    public int CompanyId { get; set; }

    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; } = "";

    [JsonPropertyName("org_number")]
    public string OrgNumber { get; set; } = "";

    [JsonPropertyName("city")]
    public string City { get; set; } = "";

    [JsonPropertyName("contact_person")]
    public string ContactPerson { get; set; } = "";

    [JsonPropertyName("contact_person_email")]
    public string ContactPersonEmail { get; set; } = "";

    [JsonPropertyName("contact_person_phone")]
    public string ContactPersonPhone { get; set; } = "";

    [JsonPropertyName("is_applied")]
    public bool IsApplied { get; set; }

    [JsonPropertyName("applied_at")]
    public DateTime? AppliedAt { get; set; }

    [JsonPropertyName("company_responded")]
    public bool CompanyResponded { get; set; }
}
