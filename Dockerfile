# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY WhitePie.WebApp.sln ./
COPY WhitePie.WebApp/WhitePie.WebApp.csproj WhitePie.WebApp/
RUN dotnet restore

# Copy the remaining source code and publish
COPY WhitePie.WebApp/. WhitePie.WebApp/
WORKDIR /src/WhitePie.WebApp
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Expose port 80
EXPOSE 80

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Start the application
ENTRYPOINT ["dotnet", "WhitePie.WebApp.dll"]
