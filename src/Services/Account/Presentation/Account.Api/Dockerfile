#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Account/Presentation/Account.Api/Account.Api.csproj", "src/Services/Account/Presentation/Account.Api/"]
COPY ["src/BuildingBlock/EventBus.Message/EventBus.Message.csproj", "src/BuildingBlock/EventBus.Message/"]
COPY ["src/Services/Account/IoC/Account.IoC/Account.IoC.csproj", "src/Services/Account/IoC/Account.IoC/"]
COPY ["src/Services/Account/Repository/Account.Repository/Account.Repository.csproj", "src/Services/Account/Repository/Account.Repository/"]
COPY ["src/Services/Account/Core/Account.Core/Account.Core.csproj", "src/Services/Account/Core/Account.Core/"]
COPY ["src/Services/Account/Service/Account.Service/Account.Service.csproj", "src/Services/Account/Service/Account.Service/"]
COPY ["src/Services/Basket/Application/Basket.Application/Basket.Application.csproj", "src/Services/Basket/Application/Basket.Application/"]
COPY ["src/Services/Basket/DataAccess/Basket.DataAccess/Basket.DataAccess.csproj", "src/Services/Basket/DataAccess/Basket.DataAccess/"]
COPY ["src/Services/Basket/Core/Basket.Core/Basket.Core.csproj", "src/Services/Basket/Core/Basket.Core/"]
COPY ["src/Services/Product/Presentation/Product.Grpc/Product.Grpc.csproj", "src/Services/Product/Presentation/Product.Grpc/"]
COPY ["src/Services/Product/Core/Product.Application/Product.Application.csproj", "src/Services/Product/Core/Product.Application/"]
COPY ["src/Services/Product/Infrastructure/Product.Persistence/Product.Persistence.csproj", "src/Services/Product/Infrastructure/Product.Persistence/"]
COPY ["src/Services/Product/Core/Product.Domain/Product.Domain.csproj", "src/Services/Product/Core/Product.Domain/"]
COPY ["src/Services/Product/IoC/Product.IoC/Product.IoC.csproj", "src/Services/Product/IoC/Product.IoC/"]
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