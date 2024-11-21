# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO/SERVPRO.csproj

RUN dotnet clean SERVPRO/SERVPRO.csproj -c Release
RUN dotnet build SERVPRO/SERVPRO.csproj -c Release
RUN dotnet publish SERVPRO/SERVPRO.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p /app/DataProtection-Keys

RUN mkdir -p /app/FotosClientes

EXPOSE 5000

ENTRYPOINT ["dotnet", "SERVPRO.dll"]
