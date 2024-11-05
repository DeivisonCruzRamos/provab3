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
COPY ["IPretty.Consumer/IPretty.Consumer.csproj", "IPretty.Consumer/"]
COPY ["IPretty.Application/IPretty.Application.csproj", "IPretty.Application/"]
COPY ["IPretty.Client/IPretty.Client.csproj", "IPretty.Client/"]
COPY ["IPretty.Domain/IPretty.Domain.csproj", "IPretty.Domain/"]
COPY ["IPretty.Service/IPretty.Service.csproj", "IPretty.Service/"]
COPY ["IPretty.Infra.MySql/IPretty.Infra.MySql.csproj", "IPretty.Infra.MySql/"]

RUN dotnet restore "IPretty.Consumer/IPretty.Consumer.csproj"
COPY . .


WORKDIR "/src/IPretty.Consumer"
RUN dotnet build "IPretty.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IPretty.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet IPretty.Consumer.dll
