#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CustomerAPI/Or.Micro.Customers.csproj", "CustomerAPI/"]
COPY ["DbContext/Or.Domain.Storage.csproj", "DbContext/"]
COPY ["Domane.Model/Or.Domain.Model.csproj", "Domane.Model/"]
RUN dotnet restore "CustomerAPI/Or.Micro.Customers.csproj"
COPY . .
WORKDIR "/src/CustomerAPI"
RUN dotnet build "Or.Micro.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Or.Micro.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Or.Micro.Customers.dll"]
