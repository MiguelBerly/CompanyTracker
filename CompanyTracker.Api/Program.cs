using System.Text.Json;
using CompanyTracker.Api.Api;
using CompanyTracker.Api.Data;
using CompanyTracker.Api.Domain.Repositories;
using CompanyTracker.Api.Data.Repositories;
using CompanyTracker.Api.Domain;
using CompanyTracker.Api.Domain.DTO;
using CompanyTracker.Api.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = "http://localhost:5400";
    options.RequireHttpsMetadata = false;
    
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5400",
        
        ValidateAudience = true,
        ValidAudience = "http://localhost:5400",
        
        ValidateLifetime = true,
        
        ValidateIssuerSigningKey = true,
        
        RoleClaimType = "role",
        
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("Member", policy => policy.RequireRole("member"));
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if(builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("CompanyTrackerTestDb");
    }
    else
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document, null),
            new List<string>()
        }
    });
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddSingleton<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<CompanyManager>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        
        logger.LogError(exception, "Unhandled error occured. Message : {Message}", exception?.Message);

        var statusCode = 500;
        var message = "Internal server error";

        if (exception is ApiException apiException)
        {
            statusCode = apiException.StatusCode;
            message = apiException.Message;
        }
        
        context.Response.StatusCode = statusCode;
        
        var errorResponse = new ErrorResponse
        {
            Success = false,
            StatusCode = statusCode,
            Message = message
        };
        
        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnablePersistAuthorization();
    });
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
        await DbSeeder.SeedAsync(dbContext);
    }
}

AppEndPoints.Map(app);


app.Run();


