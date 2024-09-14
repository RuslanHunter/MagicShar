# Этап базы
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копирование проектных файлов и восстановление зависимостей
COPY ["MagicShar.Web/MagicShar.Web.csproj", "MagicShar.Web/"]
COPY ["MagicSharModels/MagicShar.Models.csproj", "MagicSharModel/"]
COPY ["MagicSharTest/MagicShar.Test.csproj", "MagicSharTest/"]
RUN dotnet restore "MagicShar.Web/MagicShar.Web.csproj"

# Копирование остального кода
COPY . .

# Сборка проекта
WORKDIR "/src/MagicShar.Web"
RUN dotnet build "MagicShar.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "MagicShar.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MagicShar.Web.dll"]