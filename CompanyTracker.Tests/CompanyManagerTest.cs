using CompanyTracker.Domain;
using CompanyTracker.Domain.DTO;
using CompanyTracker.Domain.Repositories;
using CompanyTracker.Exceptions;
using Moq;

namespace CompanyTracker.Tests;

public class CompanyManagerTest
{
    private readonly Mock<IServicesRepository> _servicesRepositoryMock;
    private readonly CompanyManager _companyTracker;

    public CompanyManagerTest()
    {
        _servicesRepositoryMock = new Mock<IServicesRepository>();
        _companyTracker = new CompanyManager(_servicesRepositoryMock.Object);
    }
    
    [Fact]
    public async Task AddCompany_ShouldThrowBadRequestException_WhenCompanyNameIsMissing()
    {
        var newCompanyDto = new NewCompany(
            "",
            "556677-8899",
            "Stockholm",
            "Anna",
            "anna@example.com",
            "0701234567");

        await Assert.ThrowsAsync<BadRequestException>(() => _companyTracker.AddCompanyAsync(newCompanyDto));

    }

    [Fact]
    public async Task AddCompany_ShouldWorkProperly()
    {
        var newCompany = new NewCompany(
            "Test Company",
            "556677-8899",
            "Stockholm",
            "Anna",
            "anna@example.com",
            "0701234567");

        _servicesRepositoryMock.Setup(x => x.CompanyExistsAsync(newCompany.OrgNumber))
            .ReturnsAsync(false);
        
        _servicesRepositoryMock.Setup(x => x.AddCompanyAsync(newCompany))
            .ReturnsAsync(1);
        
        var result = await _companyTracker.AddCompanyAsync(newCompany);
        
        Assert.Equal(1, result.CompanyId);
        Assert.Equal(newCompany.CompanyName, result.CompanyName);
        Assert.Equal(newCompany.OrgNumber, result.OrgNumber);
        Assert.Equal(newCompany.City, result.City);

    }

    [Fact]
    public async Task GetNotAppliedCompanies_ShouldReturnCompaniesFromRepository()
    {
        var expectedCompanies = new List<NotAppliedCompany>
        {
            new()
            {
                Name =  "First Test Company",
                ContactPerson = "Anna",
                ContactEmail = "anna@example.com",
                IsApplied = false
            },
            new()
            {
            Name =  "Second Test Company",
            ContactPerson = "Anna",
            ContactEmail = "anna@example.com",
            IsApplied = true
            }
        };
        
        _servicesRepositoryMock.Setup(x => x.GetNotAppliedCompaniesAsync())
            .ReturnsAsync(expectedCompanies);
        
        var result = await _companyTracker.GetNotAppliedCompaniesAsync();
        
        Assert.Equal(expectedCompanies, result);
    }

    [Fact]
    public async Task GetCompanyById_ShouldReturnCompanyFromRepository()
    {
        var expectedCompany = new CompanyDetails
        {
            CompanyId = 1,
            CompanyName = "Test Company",
            OrgNumber = "556677-8899",
            City = "Stockholm",
            ContactPerson = "Anna",
            ContactPersonEmail = "anna@example.com",
            ContactPersonPhone = "0701234567",
            IsApplied = true,
            CompanyResponded = false
        };

        _servicesRepositoryMock.Setup(x => x.GetCompanyByIdAsync(1))
            .ReturnsAsync(expectedCompany);

        var result = await _companyTracker.GetCompanyByIdAsync(1);

        Assert.Equal(expectedCompany, result);
    }
}
