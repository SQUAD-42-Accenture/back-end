# Etapa base para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Servpro/Servpro.sln", "./"]
COPY ["Servpro/", "Servpro/"]
WORKDIR "/src/Servpro"
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Servpro.dll"]
