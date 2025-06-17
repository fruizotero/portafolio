# Establecer la imagen base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Establecer la imagen de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["portafolio.backend.API.csproj", "./"]
RUN dotnet restore "portafolio.backend.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "portafolio.backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "portafolio.backend.API.csproj" -c Release -o /app/publish

# Establecer la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "portafolio.backend.API.dll"]