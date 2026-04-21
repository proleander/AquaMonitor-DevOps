# ================================
# ETAPA 1 — BUILD (.NET 10 SDK)
# ================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia o arquivo de projeto
COPY ["AquaMonitor.Api.csproj", "./"]

# Restaura dependências
RUN dotnet restore "AquaMonitor.Api.csproj"

# Copia todo o conteúdo da pasta atual
COPY . .

# Compila e Publica em modo Release

RUN dotnet publish "AquaMonitor.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ================================
# ETAPA 2 — RUNTIME (.NET 10)
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app


COPY --from=build /app/publish .

# No .NET 10, a porta padrão é 8080 (não-root)
EXPOSE 8080

# Inicia a aplicação
ENTRYPOINT ["dotnet", "AquaMonitor.Api.dll"]