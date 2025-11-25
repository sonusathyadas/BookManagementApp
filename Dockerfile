# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY BookManagementAPI.sln .
COPY BookManagementAPI.API/BookManagementAPI.API.csproj BookManagementAPI.API/
COPY BookManagementAPI.Core/BookManagementAPI.Core.csproj BookManagementAPI.Core/
COPY BookManagementAPI.Infrastructure/BookManagementAPI.Infrastructure.csproj BookManagementAPI.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build the application
WORKDIR /src/BookManagementAPI.API
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080

# Copy published files from publish stage
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Define the entry point
ENTRYPOINT ["dotnet", "BookManagementAPI.API.dll"]
