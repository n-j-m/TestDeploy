FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY TestDeploy.Api/*.csproj TestDeploy.Api/
RUN dotnet restore
COPY . .

FROM build AS publish
WORKDIR /src/TestDeploy.Api
RUN dotnet public -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/public .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TestDeploy.Api.dll
