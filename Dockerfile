FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./Servpro ./Servpro
WORKDIR /src/Servpro
RUN dotnet restore Servpro.sln
RUN dotnet publish Servpro.sln -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Servpro.dll"]
