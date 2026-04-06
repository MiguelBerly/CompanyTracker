using System.Net;
using System.Net.Http.Json;
using CompanyTracker.Api.Data;
using CompanyTracker.Api.Domain.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyTracker.Tests;

public class CompanyManagerApiTests : IClassFixture<CompanyTrackerWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CompanyManagerApiTests(CompanyTrackerWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    

    [Fact]
    public async Task GetAllCompaniesWithApplications_ReturnsEmptyList()
    {
        var response = await _client.GetAsync("api/companies/applications");
        response.EnsureSuccessStatusCode();

        var companies = await response.Content.ReadFromJsonAsync<List<CompanyWithApplications>>();
        
        Assert.NotNull(companies);
        Assert.Empty(companies);
    }

    [Fact]
    public async Task CreateCompany_ThenGetCompany_ReturnsCompany()
    {
        var newCompany = new NewCompany(
            "Test Company",
            "556677-8899",
            "Stockholm",
            "Anna Andersson",
            "anna@example.com",
            "0701234567");

        var createResponse = await _client.PostAsJsonAsync("api/companies/", newCompany);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdCompany = await createResponse.Content.ReadFromJsonAsync<CreatedCompany>();

        Assert.NotNull(createdCompany);
        Assert.True(createdCompany.CompanyId > 0);
        Assert.Equal(newCompany.CompanyName, createdCompany.CompanyName);
        Assert.Equal(newCompany.OrgNumber, createdCompany.OrgNumber);
        Assert.Equal(newCompany.City, createdCompany.City);

        var getResponse = await _client.GetAsync($"api/companies/{createdCompany.CompanyId}");
        getResponse.EnsureSuccessStatusCode();

        var company = await getResponse.Content.ReadFromJsonAsync<CompanyDetails>();

        Assert.NotNull(company);
        Assert.Equal(createdCompany.CompanyId, company.CompanyId);
        Assert.Equal(newCompany.CompanyName, company.CompanyName);
        Assert.Equal(newCompany.OrgNumber, company.OrgNumber);
        Assert.Equal(newCompany.City, company.City);
        Assert.Equal(newCompany.ContactPerson, company.ContactPerson);
        Assert.Equal(newCompany.ContactPersonEmail, company.ContactPersonEmail);
        Assert.Equal(newCompany.ContactPersonPhone, company.ContactPersonPhone);
        Assert.False(company.IsApplied);
        Assert.Null(company.AppliedAt);
        Assert.False(company.CompanyResponded);
    }
    
    
    
}
