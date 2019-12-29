FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
COPY ..
CMD ASPNETCORE_URLS=http://*:$PORT dotnet KaylaaShop.dll