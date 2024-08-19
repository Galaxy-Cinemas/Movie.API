#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Instalar netcat
# RUN apt-get update && apt-get install -y netcat

USER app
WORKDIR /app
EXPOSE 8080

# Crear la carpeta para los logs dentro del contenedor
RUN mkdir -p /app/samba/logs

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Galaxi.Movie.API/Galaxi.Movie.API.csproj", "Galaxi.Movie.API/"]
COPY ["Galaxi.Movie.Domain/Galaxi.Movie.Domain.csproj", "Galaxi.Movie.Domain/"]
COPY ["Galaxi.Bus.Message/Galaxi.Bus.Message.csproj", "Galaxi.Bus.Message/"]
COPY ["Galaxi.Movie.Persistence/Galaxi.Movie.Persistence.csproj", "Galaxi.Movie.Persistence/"]
COPY ["Galaxi.Movie.Data/Galaxi.Movie.Data.csproj", "Galaxi.Movie.Data/"]
RUN dotnet restore "./Galaxi.Movie.API/./Galaxi.Movie.API.csproj"
COPY . .
WORKDIR "/src/Galaxi.Movie.API"
RUN dotnet build "./Galaxi.Movie.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Galaxi.Movie.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Galaxi.Movie.API.dll"]
