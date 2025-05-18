# Use .NET SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY DiscordBotApp/*.csproj ./DiscordBotApp/
RUN dotnet restore DiscordBotApp/DiscordBotApp.csproj

COPY . .
WORKDIR /app/DiscordBotApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/DiscordBotApp/out .

ENV DISCORD_TOKEN=changeme

ENTRYPOINT ["dotnet", "DiscordBotApp.dll"]