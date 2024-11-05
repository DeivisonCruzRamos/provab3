#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build

ARG SSH_KEY
ARG GIT_REPO_NAME

ENV SSH_KEY=${SSH_KEY}
ENV GIT_REPO_NAME=${GIT_REPO_NAME}

COPY install_nuget_pkg.sh /tmp/install_nuget_pkg.sh
RUN chmod +x /tmp/install_nuget_pkg.sh

RUN apt-get update && apt-get install -y dos2unix

# Ensure Unix line endings for the script
RUN dos2unix /tmp/install_nuget_pkg.sh

RUN ["/bin/bash", "-c", "/tmp/install_nuget_pkg.sh"] || true

WORKDIR /src
COPY ["B3.Api/B3.Api.csproj", "B3.Api/"]
COPY ["B3.Application/B3.Application.csproj", "B3.Application/"]
COPY ["B3.Client/B3.Client.csproj", "B3.Client/"]
COPY ["B3.Domain/B3.Domain.csproj", "B3.Domain/"]
COPY ["B3.Service/B3.Service.csproj", "B3.Service/"]
COPY ["B3.Infra.MySql/B3.Infra.MySql.csproj", "B3.Infra.MySql/"]
RUN dotnet restore "B3.Api/B3.Api.csproj"
COPY . .
WORKDIR "/src/B3.Api"
RUN dotnet build "B3.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "B3.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet B3.Api.dll