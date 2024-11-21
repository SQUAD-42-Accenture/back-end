FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO.sln

# Publicação do projeto individualmente, com a opção --no-restore e desabilitando a geração de web assets
RUN dotnet publish SERVPRO/SERVPRO.csproj -c Release -o /app/publish --no-build --no-restore -nowarn:CS8618,CS8981

RUN mkdir -p /app/FotosClientes
COPY SERVPRO/SERVPRO/FotosClientes/ /app/FotosClientes/

RUN ls -l /app/FotosClientes

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

RUN ls -l /app/FotosClientes

RUN chmod -R 755 /app/FotosClientes

ENTRYPOINT ["dotnet", "SERVPRO.dll"]
