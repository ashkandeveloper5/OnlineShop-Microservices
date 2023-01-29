#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Account/Presentation/Account.Api/Account.Api.csproj", "src/Services/Account/Presentation/Account.Api/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["src/Services/Account/IoC/Account.IoC/Account.IoC.csproj", "src/Services/Account/IoC/Account.IoC/"]
COPY ["src/Services/Account/Repository/Account.Repository/Account.Repository.csproj", "src/Services/Account/Repository/Account.Repository/"]
COPY ["src/Services/Account/Core/Account.Core/Account.Core.csproj", "src/Services/Account/Core/Account.Core/"]
COPY ["src/Services/Account/Service/Account.Service/Account.Service.csproj", "src/Services/Account/Service/Account.Service/"]
RUN dotnet restore "src/Services/Account/Presentation/Account.Api/Account.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Account/Presentation/Account.Api"
RUN dotnet build "Account.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.Api.dll"]