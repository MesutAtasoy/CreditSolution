#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Email/Email.Api/Email.Api.csproj", "Email/Email.Api/"]
RUN dotnet restore "Email/Email.Api/Email.Api.csproj"
COPY . .
WORKDIR "/src/Email/Email.Api"
RUN dotnet build "Email.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Email.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Email.Api.dll"]