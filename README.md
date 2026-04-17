# CompanyTracker

A fullstack application for tracking companies and job applications, built with ASP.NET Core, PostgreSQL, and a minimal HTML frontend.

**Live site:** https://companytracker.onrender.com/index.html

---

## Stack

- **Backend:** .NET 10 ASP.NET Core Web API
- **Database:** PostgreSQL (Docker locally, managed Render Postgres in production)
- **Frontend:** Static HTML served by the API
- **Tests:** xUnit (unit tests + integration tests)
- **CI/CD:** GitHub Actions + Render

## CI/CD Pipeline

```
PR to main   → CI: build + test
Push to main → CD: bump version → build Docker image → push to ghcr.io → deploy to Render → create GitHub Release
```

## Local Development

**Prerequisites:** .NET 10 SDK, Docker Desktop

Start the database:

```bash
docker compose up -d
```

Run the API:

```bash
cd CompanyTracker.Api
dotnet run --launch-profile http
```

- API: `http://localhost:5400`
- Swagger: `http://localhost:5400/swagger`
- Frontend: `http://localhost:5400/index.html`

Migrations and seed data run automatically on startup in Development.

## Project Structure

```
Api_ex_2/
  CompanyTracker.Api/       # ASP.NET Core API + static frontend
  CompanyTracker.Tests/     # xUnit tests
  docker-compose.yml        # Local Postgres
  .github/workflows/
    CI.yml                  # Build + test on PR to main
    CD.yml                  # Version bump, Docker build, deploy on push to main
```