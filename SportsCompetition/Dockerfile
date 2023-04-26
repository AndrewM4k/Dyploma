FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Migartions.csproj .
COPY . .
RUN dotnet restore "SportsCompetition.csproj"

FROM build AS publish
WORKDIR /src
RUN dotnet publish "SportsCompetition.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app/run
COPY --from=publish /app/publish .
ENTRYPOINT	["dotnet", "SportsCompetition.dll"]