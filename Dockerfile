FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copie o arquivo de solução .sln e o código do projeto
COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

# Restauração das dependências
RUN dotnet restore SERVPRO.sln

# Build do projeto
RUN dotnet build SERVPRO/SERVPRO.csproj -c Release -o /app/build

# Publicação do projeto
RUN dotnet publish SERVPRO/SERVPRO.csproj -c Release -o /app/publish --no-build --no-restore

# Criando diretório para arquivos estáticos se necessário
RUN mkdir -p /app/FotosClientes
COPY SERVPRO/SERVPRO/FotosClientes/ /app/FotosClientes/

# Imagem de runtime com aspnet
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copia os artefatos publicados
COPY --from=build /app/publish .

# Definindo as permissões corretas, se necessário
RUN chmod -R 755 /app/FotosClientes

# Configuração do entrypoint
ENTRYPOINT ["dotnet", "SERVPRO.dll"]
