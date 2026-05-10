# Multi-stage Dockerfile for WhatsReal API

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ["WhatsReal.sln", "."]
COPY ["src/Api/WhatsReal.Api.csproj", "src/Api/"]
COPY ["src/Application/WhatsReal.Application.csproj", "src/Application/"]
COPY ["src/Domain/WhatsReal.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/WhatsReal.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Shared/WhatsReal.Shared.csproj", "src/Shared/"]

# Restore dependencies
RUN dotnet restore "WhatsReal.sln"

# Copy source code
COPY . .

# Build
RUN dotnet build "WhatsReal.sln" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "src/Api/WhatsReal.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Create logs directory
RUN mkdir -p /app/logs

EXPOSE 5000 5001

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "WhatsReal.Api.dll"]
