#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Sms/Sms.Api/Sms.Api.csproj", "Sms/Sms.Api/"]
COPY ["CreditSolutionFramework/Framework/Framework.csproj", "CreditSolutionFramework/Framework/"]
COPY ["Sms/Sms.Api/nuget.config", "Sms/Sms.Api/"]

RUN dotnet restore "Sms/Sms.Api/Sms.Api.csproj" --configfile "Sms/Sms.Api/nuget.config" --disable-parallel
COPY . .

WORKDIR "/src/Sms/Sms.Api"
RUN dotnet build "Sms.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sms.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sms.Api.dll"]