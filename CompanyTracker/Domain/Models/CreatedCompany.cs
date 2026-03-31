using System.Text.Json.Serialization;

namespace CompanyTracker.Domain.DTO;

public record CreatedCompany(
    [property: JsonPropertyName("company_id")]
    int CompanyId,

    [property: JsonPropertyName("company_name")]
    string CompanyName,

    [property: JsonPropertyName("org_number")]
    string OrgNumber,

    [property: JsonPropertyName("city")]
    string City
);

