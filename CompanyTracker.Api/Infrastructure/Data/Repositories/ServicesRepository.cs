using CompanyTracker.Api.Data.Entities;
using CompanyTracker.Api.Domain.DTO;
using CompanyTracker.Api.Domain.Repositories;
using CompanyTracker.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CompanyTracker.Api.Data.Repositories;

public class ServicesRepository : IServicesRepository
{
    private readonly AppDbContext _dbContext;

    public ServicesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CompanyExistsAsync(string orgNumber)
    {
        return await _dbContext.Companies.AnyAsync(c => c.OrgNumber == orgNumber);
    }

    public async Task<int> AddCompanyAsync(NewCompany newCompany)
    {
        var company = new CompanyEntity
        {
            Name = newCompany.CompanyName,
            OrgNumber = newCompany.OrgNumber,
            City = newCompany.City,
            ContactInfos = new CompanyContactInfoEntity
            {
                ContactPerson = newCompany.ContactPerson,
                ContactPersonEmail = newCompany.ContactPersonEmail,
                ContactPersonPhone = newCompany.ContactPersonPhone
            },
            Applications = new AppliedToCompanyEntity
            {
                IsApplied = false,
                CompanyResponded = false
            }
        };
        
        await _dbContext.AddAsync(company);
        await _dbContext.SaveChangesAsync();

        return company.Id;
    }

    public async Task<CompanyDetails> GetCompanyByIdAsync(int companyId)
    {
        var company = await _dbContext.Companies
            .Include(c => c.ContactInfos)
            .Include(c => c.Applications)
            .Where(c => c.Id == companyId)
            .Select(c => new CompanyDetails
            {
                CompanyId = c.Id,
                CompanyName = c.Name,
                OrgNumber = c.OrgNumber,
                City = c.City,
                ContactPerson = c.ContactInfos.ContactPerson,
                ContactPersonEmail = c.ContactInfos.ContactPersonEmail,
                ContactPersonPhone = c.ContactInfos.ContactPersonPhone,
                IsApplied = c.Applications.IsApplied,
                AppliedAt = c.Applications.AppliedAt,
                CompanyResponded = c.Applications.CompanyResponded
            })
            .FirstOrDefaultAsync();

        if (company == null)
            throw new ObjectNotFoundException(companyId);

        return company;
    }

    public async Task<List<NotAppliedCompany>> GetNotAppliedCompaniesAsync()
    {
        return await _dbContext.Companies
            .Where(c => !c.Applications.IsApplied)
            .Select(c => new NotAppliedCompany()
            {
                CompanyId = c.Id,
                Name = c.Name,
                ContactPerson = c.ContactInfos.ContactPerson,
                ContactEmail = c.ContactInfos.ContactPersonEmail,
                IsApplied = c.Applications.IsApplied
            })
            .ToListAsync();
    }

    public async Task DeleteCompanyAsync(int companyId)
    {
       var company = await _dbContext.Companies
           .FirstOrDefaultAsync(c => c.Id == companyId);

       if (company == null) throw new ObjectNotFoundException(companyId);
       
       _dbContext.Companies.Remove(company);
       await _dbContext.SaveChangesAsync();

    }

    public async Task<List<CompanyWithApplications>> GetAllCompaniesWithApplicationsAsync()
    {
        var companies = await _dbContext.Companies
            .Include(c => c.Applications)
            .Select(x => new CompanyWithApplications
            {
                CompanyId = x.Id,
                Name =  x.Name,
                Applied =  x.Applications.IsApplied
            }).ToListAsync();
        
        return companies;
    }

    public async Task UpdateAppliedToCompanyAsync(int companyId)
    {
        var companyApplication = _dbContext.AppliedToCompany
            .FirstOrDefault(c => c.CompanyId == companyId);
        
        if (companyApplication == null)
            throw new ObjectNotFoundException(companyId);
        
        companyApplication.IsApplied = true;
        companyApplication.AppliedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateApplicationAnsweredAsync(int companyId)
    {
        var companyApplication = _dbContext.AppliedToCompany
            .FirstOrDefault(c => c.CompanyId == companyId);
        
        if (companyApplication == null)
            throw new ObjectNotFoundException(companyId);
        
        companyApplication.CompanyResponded = true;
        
        await _dbContext.SaveChangesAsync();
    }
}

