#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ProductApi/Or.Micro.Products.csproj", "ProductApi/"]
COPY ["DbContext/Or.Domain.Storage.csproj", "DbContext/"]
COPY ["Domane.Model/Or.Domain.Model.csproj", "Domane.Model/"]
RUN dotnet restore "ProductApi/Or.Micro.Products.csproj"
COPY . .
WORKDIR "/src/ProductApi"
RUN dotnet build "Or.Micro.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Or.Micro.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Or.Micro.Products.dll"]
