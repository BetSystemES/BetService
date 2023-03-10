FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BetService.Grpc/BetService.Grpc.csproj", "src/BetService.Grpc/"]
COPY ["src/BetService.BusinessLogic/BetService.BusinessLogic.csproj", "src/BetService.BusinessLogic/"]
COPY ["src/BetService.DataAccess/BetService.DataAccess.csproj", "src/BetService.DataAccess/"]
RUN dotnet restore "src/BetService.Grpc/BetService.Grpc.csproj"
COPY . .
WORKDIR "/src/src/BetService.Grpc"
RUN dotnet build "BetService.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BetService.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BetService.Grpc.dll"]