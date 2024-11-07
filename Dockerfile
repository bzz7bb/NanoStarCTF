# Use the .NET SDK 8.0 as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy your application files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build and publish your application
RUN dotnet publish -c Release -o out

# Build the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /app/out .

# Set the entry point
ENTRYPOINT ["dotnet", "NanoStarCTF.Api.dll"]
