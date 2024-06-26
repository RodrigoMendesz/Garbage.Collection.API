#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Garbage.Collection.API/Garbage.Collection.API.csproj", "Garbage.Collection.API/"]
COPY ["Garbage.Collection.Business/Garbage.Collection.Business.csproj", "Garbage.Collection.Business/"]
COPY ["Garbage.Collection.Data/Garbage.Collection.Data.csproj", "Garbage.Collection.Data/"]
RUN dotnet restore "./Garbage.Collection.API/Garbage.Collection.API.csproj"
COPY . .
WORKDIR "/src/Garbage.Collection.API"
RUN dotnet build "./Garbage.Collection.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Garbage.Collection.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Garbage.Collection.API.dll"]