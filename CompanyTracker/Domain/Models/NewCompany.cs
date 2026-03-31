using System.Text.Json.Serialization;

namespace CompanyTracker.Domain.DTO;

public record NewCompany(
    [property: JsonPropertyName("company_name")]
    string CompanyName,

    [property: JsonPropertyName("org_number")]
    string OrgNumber,

    [property: JsonPropertyName("city")]
    string City,

    [property: JsonPropertyName("contact_person")]
    string ContactPerson,

    [property: JsonPropertyName("contact_person_email")]
    string ContactPersonEmail,

    [property: JsonPropertyName("contact_person_phone")]
    string ContactPersonPhone
    );

