using System.ComponentModel.DataAnnotations;

namespace CompanyTracker.Data.Entities;

public class CompanyEntity
{
    public int Id { get; set; }
    [MaxLength(30)]
    public required string Name { get; set; }
    [MaxLength(30)]
    public required string OrgNumber { get; set; }
    [MaxLength(30)]
    public required string City { get; set; }

    public CompanyContactInfoEntity ContactInfos { get; set; }
    public AppliedToCompanyEntity Applications { get; set; } 
}
