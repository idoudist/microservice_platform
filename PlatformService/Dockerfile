# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
#EXPOSE 80
#EXPOSE 443

# Copy the CSPROJ file and restore any dependencies (via NUGGET)
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build our release
COPY . ./
RUN dotnet publish -c release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]

