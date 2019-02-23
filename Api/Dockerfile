FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 54422

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Api/Api.csproj Api/
RUN dotnet restore Api/Api.csproj
COPY . .
WORKDIR /src/Api
RUN dotnet build Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.dll"]
