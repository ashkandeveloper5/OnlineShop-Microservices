#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Product/Presentation/Product.Api/Product.Api.csproj", "src/Services/Product/Presentation/Product.Api/"]
COPY ["src/Services/Product/Common/Product.Common/Product.Common.csproj", "src/Services/Product/Common/Product.Common/"]
COPY ["src/Services/Product/Core/Product.Domain/Product.Domain.csproj", "src/Services/Product/Core/Product.Domain/"]
COPY ["src/Services/Product/Presentation/Product.Grpc/Product.Grpc.csproj", "src/Services/Product/Presentation/Product.Grpc/"]
COPY ["src/Services/Product/Core/Product.Application/Product.Application.csproj", "src/Services/Product/Core/Product.Application/"]
COPY ["src/Services/Product/Infrastructure/Product.Persistence/Product.Persistence.csproj", "src/Services/Product/Infrastructure/Product.Persistence/"]
COPY ["src/Services/Product/IoC/Product.IoC/Product.IoC.csproj", "src/Services/Product/IoC/Product.IoC/"]
RUN dotnet restore "src/Services/Product/Presentation/Product.Api/Product.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Product/Presentation/Product.Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Api.dll"]