FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar o arquivo da solução e os projetos
COPY SERVPRO/SERVPRO.sln ./SERVPRO.sln
COPY SERVPRO/SERVPRO/ ./SERVPRO/

# Restaurar as dependências do NuGet
RUN dotnet restore SERVPRO/SERVPRO.csproj

# Limpar, compilar e publicar o projeto
RUN dotnet clean SERVPRO/SERVPRO.csproj -c Release
RUN dotnet build SERVPRO/SERVPRO.csproj -c Release
RUN dotnet publish SERVPRO/SERVPRO.csproj -c Release -o /app/publish --no-restore

# Imagem final para execução do aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados do estágio anterior
COPY --from=build /app/publish .

# Criar diretório para fotos de clientes
RUN mkdir -p /app/FotosClientes

# Expor a porta do contêiner
EXPOSE 80

# Definir o ponto de entrada do contêiner
ENTRYPOINT ["dotnet", "SERVPRO.dll"]
