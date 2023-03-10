FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BetService.DatabaseMigrator/BetService.DatabaseMigrator.csproj", "src/BetService.DatabaseMigrator/"]
COPY ["src/BetService.DataAccess/BetService.DataAccess.csproj", "src/BetService.DataAccess/"]
COPY ["src/BetService.BusinessLogic/BetService.BusinessLogic.csproj", "src/BetService.BusinessLogic/"]
RUN dotnet restore "src/BetService.DatabaseMigrator/BetService.DatabaseMigrator.csproj"
COPY . .
WORKDIR "/src/src/BetService.DatabaseMigrator"
RUN dotnet build "BetService.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BetService.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
