using System.Net;
using CompanyTracker.Api.Domain;
using CompanyTracker.Api.Domain.DTO;
using CompanyTracker.Api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CompanyTracker.Api.Api;

public static class AppEndPoints
{
    public static void Map(WebApplication app)
    {

        var group = app.MapGroup("/api");

        group.MapGet("/token/{tokenType}", ([FromServices]ITokenRepository tokenRepository, string tokenType) =>
        {
            if (!Enum.TryParse<TokenType>(tokenType, true, out var parsedTokenType))
            {
                return Results.BadRequest($"Invalid token type '{tokenType}'. Allowed values are member or admin.");
            }

            var token = tokenRepository.CreateTestToken(parsedTokenType);
            
            return Results.Ok(token);
        })
        .WithName("GetToken")
        .WithTags("Test Tokens")
        .WithSummary("Get a test token")
        .WithDescription("Get a test token, valid param are member/admin");
        
        app.MapGet("/.well-known/jwks.json", ([FromServices]ITokenRepository tokenRepository) =>
        {
            var jwk = tokenRepository.PublishKey();
 
            return Results.Ok(new { keys = new [] { jwk }});
        })
        .ExcludeFromDescription();

        app.MapGet("/.well-known/openid-configuration", () =>
        {
            return Results.Ok(new
            {
                issuer = "http://localhost:5400",
                jwks_uri = "http://localhost:5400/.well-known/jwks.json",
                token_endpoint = "http://localhost:5400/api/token/{tokenType}",
                response_types_supported = new[] { "token" },
                subject_types_supported = new[] { "public" },
                id_token_signing_alg_values_supported = new[] { "RS256" }
            });
        })
        .ExcludeFromDescription();

        group.MapPost("/companies/", async (CompanyManager companyManager, NewCompany newCompany) =>
        {
            var createdCompany = await companyManager.AddCompanyAsync(newCompany);

            return Results.Created($"api/companies/{createdCompany.CompanyId}", createdCompany);
        })
        .WithName("AddCompany")
        .WithTags("Companies")
        .WithSummary("Add a new company")
        .WithDescription("Creates a company with contact information and default application tracking values.")
        .Accepts<NewCompany>("application/json")
        .Produces<CreatedCompany>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("/companies/{companyId:int}", async (CompanyManager companyManager, int companyId) =>
        {
            var company = await companyManager.GetCompanyByIdAsync(companyId);

            return Results.Ok(company);
        })
        .WithName("GetCompanyById")
        .WithTags("Companies")
        .WithSummary("Get a company by id")
        .WithDescription("Returns a company with contact information and application status.")
        .Produces<CompanyDetails>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapDelete("/companies/{companyId:int}", async (CompanyManager companyTracker, int companyId) =>
        {
            
            await companyTracker.DeleteCompanyAsync(companyId);
            
            return Results.NoContent();
        })
        .WithName("RemoveCompany")
        .WithTags("Companies")
        .WithSummary("Remove a company")
        .WithDescription("Deletes a company by id.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapGet("/companies/NotApplied", async (CompanyManager companyTracker) =>
        {
            var companies = await companyTracker.GetNotAppliedCompaniesAsync();
            
            return Results.Ok(companies);
        })
        .WithName("GetMissingApplications")
        .WithTags("Companies")
        .WithSummary("Get companies without an application [Requires Authorization]")
        .WithDescription("Returns companies where the application flag is still false.")
        .Produces<List<NotAppliedCompany>>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
        .RequireAuthorization("Admin");

        group.MapGet("/companies/applications", async (CompanyManager companyTracker) =>
        {
            var companies = await companyTracker.GetAllCompaniesWithApplicationsAsync();
    
            return Results.Ok(companies);
        })
        .WithName("GetCompaniesAndApplications")
        .WithTags("Companies")
        .WithSummary("Get all companies with application status")
        .WithDescription("Returns all companies together with their application status.")
        .Produces<List<CompanyWithApplications>>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapPatch("/companies/application/{companyId:int}", async (CompanyManager companyTracker, int companyId) =>
        {
             await companyTracker.UpdateAppliedToCompanyAsync(companyId);

             return Results.Ok();
        })
        .WithName("UpdateApplicationStatus")
        .WithTags("Companies")
        .WithSummary("Mark company as applied")
        .WithDescription("Updates the application status for a company and sets the application timestamp.")
        .Produces(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        group.MapPatch("/companies/response/{companyId:int}", async (CompanyManager companyTracker, int companyId) =>
        {
            await companyTracker.UpdateApplicationAnsweredAsync(companyId);
            
            return Results.Ok();
        })
        .WithName("UpdateRespondStatus")
        .WithTags("Companies")
        .WithSummary("Mark company as responded")
        .WithDescription("Updates the response status for a company application.")
        .Produces(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
        
    }
}
