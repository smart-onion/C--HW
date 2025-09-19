FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
 
# Устанавливаем переменную окружения для ASP.NET Core
ENV ASPNETCORE_URLS=http://+:80
 
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish
 
FROM base AS final
COPY --from=build /app/publish .
WORKDIR /app
ENTRYPOINT ["dotnet", "VotingSystem.API.dll"]