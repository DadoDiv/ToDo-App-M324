FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiere die Projektdateien und wiederherstelle Abhängigkeiten
COPY ["ToDo-App M324/ToDo-App M324.csproj", "ToDo-App M324/"]
COPY ["ToDo-App M324.Application/ToDo-App M324.Application.csproj", "ToDo-App M324.Application/"]
RUN dotnet restore "ToDo-App M324/ToDo-App M324.csproj"

# Kopiere den Rest des Codes und baue die Anwendung
COPY . ./
WORKDIR "/app/ToDo-App M324"
RUN dotnet build --no-restore -c Release
RUN dotnet publish --no-restore --no-build -c Release -o /app/publish

# Erstelle ein Laufzeit-Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Starte die Anwendung
ENTRYPOINT ["dotnet", "ToDo-App M324.dll"]