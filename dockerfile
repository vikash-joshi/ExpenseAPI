# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ./CleanArchitectureDemo.API/CleanArchitectureDemo.API.csproj ./CleanArchitectureDemo.API/
COPY ./CleanArchitectureDemo.Application/CleanArchitectureDemo.Application.csproj ./CleanArchitectureDemo.Application/
COPY ./CleanArchitectureDemo.Infrastructure/CleanArchitectureDemo.Infrastructure.csproj ./CleanArchitectureDemo.Infrastructure/
COPY ./CleanArchitectureDemo.Domain/CleanArchitectureDemo.Domain.csproj ./CleanArchitectureDemo.Domain/

RUN dotnet restore ./CleanArchitectureDemo.API/CleanArchitectureDemo.API.csproj

# Copy everything else and build
COPY . ./
WORKDIR /src/CleanArchitectureDemo.API
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "CleanArchitectureDemo.API.dll"]