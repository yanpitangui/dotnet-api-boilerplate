##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Boilerplate.Api/Boilerplate.Api.csproj", "src/Boilerplate.Api/"]
COPY ["src/Boilerplate.Application/Boilerplate.Application.csproj", "src/Boilerplate.Application/"]
COPY ["src/Boilerplate.Domain/Boilerplate.Domain.csproj", "src/Boilerplate.Domain/"]
COPY ["src/Boilerplate.Domain.Core/Boilerplate.Domain.Core.csproj", "src/Boilerplate.Domain.Core/"]
COPY ["src/Boilerplate.Infrastructure/Boilerplate.Infrastructure.csproj", "src/Boilerplate.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Boilerplate.Api"

FROM build AS publish
RUN dotnet publish "Boilerplate.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boilerplate.Api.dll"]