using CompanyTracker.Domain.DTO;
using CompanyTracker.Domain.Repositories;
using CompanyTracker.Exceptions;

namespace CompanyTracker.Domain;

public class CompanyManager(IServicesRepository servicesRepository)
{
    public async Task<CreatedCompany> AddCompanyAsync(NewCompany newCompany)
    {
        if (string.IsNullOrWhiteSpace(newCompany.CompanyName))
            throw new BadRequestException("Company name is required.");

        if (string.IsNullOrWhiteSpace(newCompany.OrgNumber))
            throw new BadRequestException("Org number is required.");

        var exists = await servicesRepository.CompanyExistsAsync(newCompany.OrgNumber);

        if (exists)
            throw new BadRequestException("A company with this org number already exists.");

        var companyId = await servicesRepository.AddCompanyAsync(newCompany);

        return new CreatedCompany(
            companyId,
            newCompany.CompanyName,
            newCompany.OrgNumber,
            newCompany.City);
    }

    public async Task<List<NotAppliedCompany>> GetNotAppliedCompaniesAsync()
    {
        return await servicesRepository.GetNotAppliedCompaniesAsync();
    }

    public async Task<CompanyDetails> GetCompanyByIdAsync(int companyId)
    {
        return await servicesRepository.GetCompanyByIdAsync(companyId);
    }

    public Task DeleteCompanyAsync(int companyId)
    {
        return servicesRepository.DeleteCompanyAsync(companyId);
    }

    public async Task<List<CompanyWithApplications>> GetAllCompaniesWithApplicationsAsync()
    {
        return await servicesRepository.GetAllCompaniesWithApplicationsAsync();
    }

    public Task UpdateAppliedToCompanyAsync(int companyId)
    {
        return servicesRepository.UpdateAppliedToCompanyAsync(companyId);
    }

    public Task UpdateApplicationAnsweredAsync(int companyId)
    {
        return servicesRepository.UpdateApplicationAnsweredAsync(companyId);
    }
}
