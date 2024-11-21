# Use a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000  # Garante que a porta 5000 seja exposta para o contêiner

# Instala o SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SERVPRO/SERVPRO.csproj", "SERVPRO/"]
RUN dotnet restore "SERVPRO/SERVPRO.csproj"
COPY . .
WORKDIR "/src/SERVPRO"
RUN dotnet build "SERVPRO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SERVPRO.csproj" -c Release -o /app/publish

# Usar a imagem do runtime para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SERVPRO.dll"]

# Definir o ambiente de execução
ENV ASPNETCORE_URLS=http://0.0.0.0:5000  # Defina explicitamente para ouvir na porta 5000
