FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln

COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO/SERVPRO.sln

RUN dotnet build SERVPRO/SERVPRO.sln -c Release -o /app/build

RUN dotnet publish SERVPRO/SERVPRO.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SERVPRO.dll"]# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln

COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO.sln

RUN dotnet build SERVPRO.sln -c Release -o /app/build

RUN dotnet publish SERVPRO.sln -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SERVPRO.dll"]