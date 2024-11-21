FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO.sln
RUN dotnet build SERVPRO.sln -c Release
RUN dotnet publish SERVPRO.sln -c Release -o /app/publish

# Etapa de runtime usando o ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SERVPRO.dll"]
