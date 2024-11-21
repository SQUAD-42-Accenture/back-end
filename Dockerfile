FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO.sln

RUN dotnet build SERVPRO.sln -c Release --no-restore -nowarn:CS8618,CS8981

RUN dotnet publish SERVPRO.sln -c Release -o /app/publish --no-build -nowarn:CS8618,CS8981

RUN mkdir -p /app/FotosClientes

COPY SERVPRO/SERVPRO/FotosClientes /app/FotosClientes

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

RUN chmod -R 755 /app/FotosClientes

ENTRYPOINT ["dotnet", "SERVPRO.dll"]
