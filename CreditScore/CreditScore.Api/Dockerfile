#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CreditScore/CreditScore.Api/CreditScore.Api.csproj", "CreditScore/CreditScore.Api/"]
COPY ["CreditScore/CreditScore.Application/CreditScore.Application.csproj", "CreditScore/CreditScore.Application/"]
COPY ["CreditScore/CreditScore.Contract/CreditScore.Contract.csproj", "CreditScore/CreditScore.Contract/"]
COPY ["CreditSolutionFramework/Framework/Framework.csproj", "CreditSolutionFramework/Framework/"]
COPY ["CreditScore/CreditScore.Persistence/CreditScore.Persistence.csproj", "CreditScore/CreditScore.Persistence/"]
COPY ["CreditScore/CreditScore.Domain/CreditScore.Domain.csproj", "CreditScore/CreditScore.Domain/"]
COPY ["CreditScore/CreditScore.Api/nuget.config", "CreditScore/CreditScore.Api/"]

RUN dotnet restore "CreditScore/CreditScore.Api/CreditScore.Api.csproj" --configfile "CreditScore/CreditScore.Api/nuget.config" --disable-parallel
COPY . .

WORKDIR "/src/CreditScore/CreditScore.Api"
RUN dotnet build "CreditScore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditScore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditScore.Api.dll"]