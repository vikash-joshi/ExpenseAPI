# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln .
COPY src/CleanArchitectureDemo.API/*.csproj ./src/CleanArchitectureDemo.API/
COPY src/CleanArchitectureDemo.Application/*.csproj ./src/CleanArchitectureDemo.Application/
COPY src/CleanArchitectureDemo.Domain/*.csproj ./src/CleanArchitectureDemo.Domain/
COPY src/CleanArchitectureDemo.Infrastructure/*.csproj ./src/CleanArchitectureDemo.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Publish the API
WORKDIR /app/src/CleanArchitectureDemo.API
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Use port 80 (Render default)
EXPOSE 80
ENTRYPOINT ["dotnet", "CleanArchitectureDemo.API.dll"]
