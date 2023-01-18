#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RabbitOracleWorker/RabbitOracleWorker.csproj", "RabbitOracleWorker/"]
RUN dotnet restore "RabbitOracleWorker/RabbitOracleWorker.csproj"
COPY . .
WORKDIR "/src/RabbitOracleWorker"
RUN dotnet build "RabbitOracleWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitOracleWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitOracleWorker.dll"]