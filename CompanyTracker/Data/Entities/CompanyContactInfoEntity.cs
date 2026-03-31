using System.ComponentModel.DataAnnotations;

namespace CompanyTracker.Data.Entities;

public class CompanyContactInfoEntity
{
    public int Id  { get; set; }
    public int CompanyId { get; set; }
    public CompanyEntity Company { get; set; }
    [MaxLength(30)]
    public string ContactPerson { get; set; } = "";
    [MaxLength(30)]
    public string ContactPersonEmail { get; set; } = "";
    [MaxLength(30)]
    public string ContactPersonPhone { get; set; } = "";
}
