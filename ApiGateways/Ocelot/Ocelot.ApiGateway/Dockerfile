FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/Ocelot/Ocelot.ApiGateway/Ocelot.ApiGateway.csproj", "ApiGateways/Ocelot/Ocelot.ApiGateway/"]

RUN dotnet restore "ApiGateways/Ocelot/Ocelot.ApiGateway/Ocelot.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Ocelot/Ocelot.ApiGateway/"
RUN dotnet build "Ocelot.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.ApiGateway.dll"]