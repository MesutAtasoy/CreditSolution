#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Credit/Credit.Api/Credit.Api.csproj", "Credit/Credit.Api/"]
COPY ["Credit/Credit.Application/Credit.Application.csproj", "Credit/Credit.Application/"]
COPY ["Credit/Credit.Domain/Credit.Domain.csproj", "Credit/Credit.Domain/"]
COPY ["CreditSolutionFramework/Framework/Framework.csproj", "CreditSolutionFramework/Framework/"]
COPY ["Credit/Credit.Persistence/Credit.Persistence.csproj", "Credit/Credit.Persistence/"]
COPY ["Credit/Credit.Contract/Credit.Contract.csproj", "Credit/Credit.Contract/"]
COPY ["Credit/Credit.UnitTest/Credit.UnitTest.csproj", "Credit/Credit.UnitTest/"]

COPY ["Credit/Credit.Api/nuget.config", "Credit/Credit.Api/"]

RUN dotnet restore "Credit/Credit.Api/Credit.Api.csproj" --configfile "Credit/Credit.Api/nuget.config" --disable-parallel
COPY . .

WORKDIR "/src/Credit/Credit.Api"
RUN dotnet build "Credit.Api.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/Credit/Credit.UnitTest"
RUN dotnet test --logger:console;verbosity=detailed

WORKDIR "/src/Credit/Credit.Api"
FROM build AS publish
RUN dotnet publish "Credit.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Credit.Api.dll"]