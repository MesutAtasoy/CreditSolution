#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CreditSolutionFramework/Framework/Framework.csproj", "CreditSolutionFramework/Framework/"]
COPY ["User/User.Api/User.Api.csproj", "User/User.Api/"]
COPY ["User/User.Persistence/User.Persistence.csproj", "User/User.Persistence/"]
COPY ["User/User.Domain/User.Domain.csproj", "User/User.Domain/"]
COPY ["User/User.Application/User.Application.csproj", "User/User.Application/"]
COPY ["User/User.Contract/User.Contract.csproj", "User/User.Contract/"]
RUN dotnet restore "User/User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]