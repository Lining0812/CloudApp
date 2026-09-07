# ===== 构建阶段 =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 仅拷 csproj 做缓存层
COPY ["CloudApp.WebApi/CloudApp.WebApi.csproj", "CloudApp.WebApi/"]
COPY ["../CloudApp.Application/CloudApp.Application.csproj", "CloudApp.Application/"]
COPY ["../CloudApp.Core/CloudApp.Core.csproj", "CloudApp.Core/"]
COPY ["../CloudApp.Infrastructure/CloudApp.Infrastructure.csproj", "CloudApp.Infrastructure/"]

RUN dotnet restore "./CloudApp.WebApi/CloudApp.WebApi.csproj"

COPY . .
WORKDIR "/src/CloudApp.WebApi"
RUN dotnet publish "./CloudApp.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== 运行阶段 =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 云托管统一 80
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:80

ENTRYPOINT ["dotnet", "CloudApp.WebApi.dll"]