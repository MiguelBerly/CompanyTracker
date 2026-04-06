using CompanyTracker.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyTracker.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Companies.AnyAsync())
        {
            return;
        }

        var companies = new List<CompanyEntity>
        {
            new()
            {
                Name = "Northwind Labs",
                OrgNumber = "556123-1001",
                City = "Stockholm",
                ContactInfos = new CompanyContactInfoEntity
                {
                    ContactPerson = "Anna Berg",
                    ContactPersonEmail = "anna.berg@northwindlabs.se",
                    ContactPersonPhone = "0701234501"
                },
                Applications = new AppliedToCompanyEntity
                {
                    IsApplied = false,
                    CompanyResponded = false
                }
            },
            new()
            {
                Name = "BluePeak Digital",
                OrgNumber = "556123-1002",
                City = "Gothenburg",
                ContactInfos = new CompanyContactInfoEntity
                {
                    ContactPerson = "Erik Lund",
                    ContactPersonEmail = "erik.lund@bluepeak.se",
                    ContactPersonPhone = "0701234502"
                },
                Applications = new AppliedToCompanyEntity
                {
                    IsApplied = true,
                    AppliedAt = DateTime.UtcNow.AddDays(-10),
                    CompanyResponded = false
                }
            },
            new()
            {
                Name = "Svea Systems",
                OrgNumber = "556123-1003",
                City = "Malmo",
                ContactInfos = new CompanyContactInfoEntity
                {
                    ContactPerson = "Sara Nilsson",
                    ContactPersonEmail = "sara.nilsson@sveasystems.se",
                    ContactPersonPhone = "0701234503"
                },
                Applications = new AppliedToCompanyEntity
                {
                    IsApplied = true,
                    AppliedAt = DateTime.UtcNow.AddDays(-20),
                    CompanyResponded = true
                }
            },
            new()
            {
                Name = "Arctic Code AB",
                OrgNumber = "556123-1004",
                City = "Uppsala",
                ContactInfos = new CompanyContactInfoEntity
                {
                    ContactPerson = "Johan Ek",
                    ContactPersonEmail = "johan.ek@arcticcode.se",
                    ContactPersonPhone = "0701234504"
                },
                Applications = new AppliedToCompanyEntity
                {
                    IsApplied = false,
                    CompanyResponded = false
                }
            },
            new()
            {
                Name = "Pixel Forge",
                OrgNumber = "556123-1005",
                City = "Vasteras",
                ContactInfos = new CompanyContactInfoEntity
                {
                    ContactPerson = "Maria Holm",
                    ContactPersonEmail = "maria.holm@pixelforge.se",
                    ContactPersonPhone = "0701234505"
                },
                Applications = new AppliedToCompanyEntity
                {
                    IsApplied = true,
                    AppliedAt = DateTime.UtcNow.AddDays(-5),
                    CompanyResponded = false
                }
            }
        };

        await context.Companies.AddRangeAsync(companies);
        await context.SaveChangesAsync();
    }
}

