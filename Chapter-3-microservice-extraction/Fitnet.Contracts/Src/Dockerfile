FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Directory.Build.props ./
COPY ["Fitnet.Contracts/Fitnet.Contracts.csproj", "Fitnet.Contracts/"]

RUN dotnet nuget add source --username your_username --password your_personal_access_token --store-password-in-clear-text --name github "https://nuget.pkg.github.com/evolutionary-architecture/index.json"

RUN dotnet restore "Fitnet.Contracts/Fitnet.Contracts.csproj"
COPY . .
WORKDIR "/src/Fitnet.Contracts"
RUN dotnet build "Fitnet.Contracts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fitnet.Contracts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvolutionaryArchitecture.Fitnet.Contracts.dll"]