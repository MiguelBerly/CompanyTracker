namespace CompanyTracker.Data.Entities;

public class AppliedToCompanyEntity
{
    public int Id { get; set; }
    public int CompanyId  { get; set; }
    public CompanyEntity Company { get; set; }
    public bool IsApplied { get; set; }
    public DateTime? AppliedAt { get; set; }
    public bool CompanyResponded  { get; set; }
    
}
