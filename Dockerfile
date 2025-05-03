# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY WhitePie.WebApp/*.csproj ./WhitePie.WebApp/
RUN dotnet restore

# Copy the rest of the code
COPY WhitePie.WebApp/. ./WhitePie.WebApp/
WORKDIR /src/WhitePie.WebApp

# Install Node.js (needed for npm/sass)
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs && \
    npm install && \
    npm run build:sass

# Build the .NET app
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (update if necessary)
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "WhitePie.WebApp.dll"]
