#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/PaymentsAPI/PaymentsAPI.csproj", "src/PaymentsAPI/"]
RUN dotnet restore "src/PaymentsAPI/PaymentsAPI.csproj"
COPY . .
WORKDIR "/src/src/PaymentsAPI"
RUN dotnet build "PaymentsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsAPI.dll"]