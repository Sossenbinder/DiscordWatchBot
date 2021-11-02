#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DiscordWatchBot/DiscordWatchBot.csproj", "DiscordWatchBot/"]
RUN dotnet restore "DiscordWatchBot/DiscordWatchBot.csproj"
COPY . .
WORKDIR "/src/DiscordWatchBot"
RUN dotnet build "DiscordWatchBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordWatchBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordWatchBot.dll"]