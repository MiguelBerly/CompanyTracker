FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY CompanyTracker.Api/CompanyTracker.Api.csproj CompanyTracker.Api/
RUN dotnet restore CompanyTracker.Api/CompanyTracker.Api.csproj
COPY . .
RUN dotnet publish CompanyTracker.Api/CompanyTracker.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CompanyTracker.Api.dll"]






