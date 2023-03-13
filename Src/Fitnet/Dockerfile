FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SuperSimpleArchitecture.Fitnet.csproj", "./"]
RUN dotnet restore "SuperSimpleArchitecture.Fitnet.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "SuperSimpleArchitecture.Fitnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperSimpleArchitecture.Fitnet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperSimpleArchitecture.Fitnet.dll"]
