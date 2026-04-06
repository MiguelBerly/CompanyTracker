using System.Collections.Immutable;
using CompanyTracker.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyTracker.Api.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<CompanyEntity> Companies =>  Set<CompanyEntity>();
    public DbSet<CompanyContactInfoEntity> CompanyContactInfo =>  Set<CompanyContactInfoEntity>();
    public DbSet<AppliedToCompanyEntity> AppliedToCompany =>  Set<AppliedToCompanyEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyEntity>(entity =>
        {
            entity.ToTable("Company");
            entity.HasKey(e => e.Id);
            
            
        });

        modelBuilder.Entity<CompanyContactInfoEntity>(entity =>
        {
            entity.ToTable("CompanyContactInfo");
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Company)
                .WithOne(x => x.ContactInfos)
                .HasForeignKey<CompanyContactInfoEntity>(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AppliedToCompanyEntity>(entity =>
        {
            entity.ToTable("CompanyAppliedCheckList");
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Company)
                .WithOne(x => x.Applications)
                .HasForeignKey<AppliedToCompanyEntity>(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
