FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

RUN dotnet restore SERVPRO.sln

# Publicação do projeto individualmente para evitar o aviso sobre --output
RUN dotnet publish SERVPRO/SERVPRO.csproj -c Release -o /app/publish --no-build -nowarn:CS8618,CS8981

# Criação do diretório FotosClientes
RUN mkdir -p /app/FotosClientes

# Copiar os arquivos da pasta FotosClientes
COPY SERVPRO/SERVPRO/FotosClientes/ /app/FotosClientes/

# Verificação para garantir que os arquivos foram copiados corretamente
RUN ls -l /app/FotosClientes

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

# Verificação para garantir que os arquivos foram copiados corretamente
RUN ls -l /app/FotosClientes

# Aplicando permissões corretamente
RUN chmod -R 755 /app/FotosClientes

ENTRYPOINT ["dotnet", "SERVPRO.dll"]
