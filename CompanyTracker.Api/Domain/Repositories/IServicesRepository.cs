using CompanyTracker.Api.Domain.DTO;

namespace CompanyTracker.Api.Domain.Repositories;

public interface IServicesRepository
{
    Task<bool> CompanyExistsAsync(string orgNumber);
    Task<int> AddCompanyAsync(NewCompany newCompany);
    Task<CompanyDetails> GetCompanyByIdAsync(int companyId);
    Task<List<NotAppliedCompany>> GetNotAppliedCompaniesAsync();
    Task DeleteCompanyAsync(int companyId);
    Task<List<CompanyWithApplications>> GetAllCompaniesWithApplicationsAsync();
    Task UpdateAppliedToCompanyAsync(int companyId);
    Task UpdateApplicationAnsweredAsync(int companyId);
}

