#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Basket/Presentation/Basket.Api/Basket.Api.csproj", "src/Services/Basket/Presentation/Basket.Api/"]
COPY ["src/BuildingBlock/EventBus.Message/EventBus.Message.csproj", "src/BuildingBlock/EventBus.Message/"]
COPY ["src/Services/Basket/Application/Basket.Application/Basket.Application.csproj", "src/Services/Basket/Application/Basket.Application/"]
COPY ["src/Services/Basket/DataAccess/Basket.DataAccess/Basket.DataAccess.csproj", "src/Services/Basket/DataAccess/Basket.DataAccess/"]
COPY ["src/Services/Basket/Core/Basket.Core/Basket.Core.csproj", "src/Services/Basket/Core/Basket.Core/"]
RUN dotnet restore "src/Services/Basket/Presentation/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Presentation/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]