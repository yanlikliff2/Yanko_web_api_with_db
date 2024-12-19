FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000   
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Yanko_web3_v2/DataAccess/DataAccess.csproj", "DataAccess/"] 
COPY ["Yanko_web3_v2/Yanko_web3_v2.csproj", "Yanko_web3_v2/"]
RUN dotnet restore "Yanko_web3_v2/Yanko_web3_v2.csproj"

COPY . .
FROM build AS publish
RUN dotnet publish "Yanko_web3_v2/Yanko_web3_v2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yanko_web3_v2.dll"]