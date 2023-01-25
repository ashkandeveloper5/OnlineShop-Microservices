#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Order/Presentation/Order.Api/Order.Api.csproj", "src/Services/Order/Presentation/Order.Api/"]
COPY ["src/Services/Order/Core/Order.Core/Order.Core.csproj", "src/Services/Order/Core/Order.Core/"]
COPY ["src/Services/Order/Data/Order.Data/Order.Data.csproj", "src/Services/Order/Data/Order.Data/"]
COPY ["src/Services/Order/Domain/Order.Domain/Order.Domain.csproj", "src/Services/Order/Domain/Order.Domain/"]
RUN dotnet restore "src/Services/Order/Presentation/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Presentation/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]