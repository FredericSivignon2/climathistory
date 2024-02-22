# Utiliser l'image de base officielle .NET SDK pour construire votre projet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copier les fichiers csproj et restaurer les dépendances
# COPY *.csproj ./
COPY *.sln .
COPY ["WeatherAPI/WeatherAPI.csproj",  "./WeatherAPI/"]
COPY ["Weather.IOC/Weather.IOC.csproj", "./Weather.IOC/"]
COPY ["Weather.Services.Postgres/Weather.Services.Database.csproj", "./Weather.Services.Postgres/"]
COPY ["Weather.Database.Postgres/Weather.Database.Postgres.csproj", "./Weather.Database.Postgres/"]
COPY ["Weather.Services.InMemory/Weather.Services.VisualCrossing.csproj", "./Weather.Services.InMemory/"]
COPY ["Weather.Database/Weather.Database.csproj", "./Weather.Database/"]
COPY ["Weather.Application/Weather.Application.csproj", "./Weather.Application/"]
COPY ["Weather.Application.VisualCrossing/Weather.Application.VisualCrossing.csproj", "./Weather.Application.VisualCrossing/"]
COPY ["WeatherAPITests/WeatherAPITests.csproj", "./WeatherAPITests/"]
COPY ["DatabaseFeeder/DatabaseFeeder.csproj", "./DatabaseFeeder/"]
COPY ["VisualCrossingDataGrabber/VisualCrossingDataGrabber.csproj", "./VisualCrossingDataGrabber/"]

RUN dotnet restore

# Copier le reste des fichiers du projet et construire l'application
COPY ["WeatherAPI/.",  "./WeatherAPI/"]
COPY ["Weather.IOC/.", "./Weather.IOC/"]
COPY ["Weather.Services.Postgres/.", "./Weather.Services.Postgres/"]
COPY ["Weather.Database.Postgres/.", "./Weather.Database.Postgres/"]
COPY ["Weather.Services.InMemory/.", "./Weather.Services.InMemory/"]
COPY ["Weather.Database/.", "./Weather.Database/"]
COPY ["Weather.Application/.", "./Weather.Application/"]
COPY ["Weather.Application.VisualCrossing/.", "./Weather.Application.VisualCrossing/"]
COPY ["WeatherAPITests/.", "./WeatherAPITests/"]
COPY ["DatabaseFeeder/.", "./DatabaseFeeder/"]
COPY ["VisualCrossingDataGrabber/.", "./VisualCrossingDataGrabber/"]

# RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;

WORKDIR /source/WeatherAPI
RUN dotnet publish -c Release -o /app --no-restore

# Générer l'image de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WeatherAPI.dll"]


#To Build the image
# docker build --pull -t weatherapi .  

# To Start the image
# docker run --rm -it -p 4000:4000 -e ASPNETCORE_URLS=https://localhost:4000 -e ASPNETCORE_ENVIRONMENT=Development -v %APPDATA%\microsoft\UserSecrets:/root/.microsoft/usersecrets -v %USERPROFILE%\.aspnet\https:/root/.aspnet/https/ weatherapi
