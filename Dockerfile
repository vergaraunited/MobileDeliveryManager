# FROM microsoft/dotnet-framework:4.7.1 AS build-env
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build-env
WORKDIR /app
COPY . .
RUN msbuild.exe /t:Build /p:Configuration=Release /p:OutputPath=out
COPY nuget.exe /app
COPY MobileDeliveryManager.csproj ./
COPY MobileDeliveryManager.sln ./
COPY *.config ./
RUN nuget.exe restore -ConfigFile nuget.config -NonInteractive MobileDeliveryManager.sln

ENTRYPOINT ["out/MobileDeliveryManager.exe"]

MAINTAINER vergara_ed@yahoo.com

EXPOSE 81
EXPOSE 8181

