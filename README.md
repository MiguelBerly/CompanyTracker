# CompanyTracker API

Det här projektet är ett ASP.NET Core Web API med Entity Framework Core och PostgreSQL.

## Förutsättningar

- .NET 10 SDK
- Docker Desktop

## Projektstruktur

- `CompanyTracker` - API-projektet
- `CompanyTracker.Tests` - tester
- `docker-compose.yml` - PostgreSQL för lokal utveckling

## Migreringar och seed-data

Projektet är konfigurerat så att detta sker automatiskt när API:t startas i `Development` med launch profile `http`:

- databasen migreras med `Database.MigrateAsync()`
- seed-data läggs in med `DbSeeder.SeedAsync()`

databasen skapas och seedas automatiskt, så länge PostgreSQL-containern är igång.

Seedningen körs bara om databasen inte redan innehåller några företag.

## Starta databasen

Gå till projektroten:

```powershell
cd "..\Api_ex_2"
```

Starta PostgreSQL i bakgrunden:

```powershell
docker compose up -d
```

Databasen startas med:

- Host: `localhost`
- Port: `5431`
- Databas: `interndb`
- Användare: `postgres`
- Lösenord: `postgres`

## Köra API:t

Gå till API-projektet:

```powershell
cd .\CompanyTracker
```

Starta API:t:

```powershell
dotnet run --launch-profile http
```

API:t kör då på:

- `http://localhost:5400`

Swagger finns på:

- `http://localhost:5400/swagger`





