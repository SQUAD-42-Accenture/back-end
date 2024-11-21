FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY SERVPRO.sln ./
COPY SERVPRO/ ./Servpro/

RUN dotnet restore
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Servpro.dll"]
