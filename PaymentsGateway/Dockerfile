#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PaymentsGateway/PaymentsGateway.csproj", "PaymentsGateway/"]
RUN dotnet restore "PaymentsGateway/PaymentsGateway.csproj"
COPY . .
WORKDIR "/src/PaymentsGateway"
RUN dotnet build "PaymentsGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsGateway.dll"]