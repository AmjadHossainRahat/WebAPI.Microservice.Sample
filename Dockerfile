# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy and restore project files
COPY src/Web/Web.csproj ./
RUN dotnet restore

# Copy the remaining files and build the app
COPY src/Web/ .
RUN dotnet build --configuration Release --no-restore

# Publish the app
RUN dotnet publish --configuration Release --no-build --output out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/out ./

# Expose the port
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "Web.dll"]
