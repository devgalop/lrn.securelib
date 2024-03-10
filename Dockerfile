FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-alpine3.18-amd64 AS base
WORKDIR /app

#build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0.201-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["lrn.devgalop.securelib.Core", "./lrn.devgalop.securelib.Core"]
COPY ["lrn.devgalop.securelib.Infrastructure", "./lrn.devgalop.securelib.Infrastructure"]
COPY ["lrn.devgalop.securelib.Webapi", "./lrn.devgalop.securelib.Webapi"]

RUN dotnet restore "./lrn.devgalop.securelib.Core/lrn.devgalop.securelib.Core.csproj"
RUN dotnet restore "./lrn.devgalop.securelib.Infrastructure/lrn.devgalop.securelib.Infrastructure.csproj"
RUN dotnet restore "./lrn.devgalop.securelib.Webapi/lrn.devgalop.securelib.Webapi.csproj"

COPY . .
WORKDIR "/src/."

RUN dotnet build "lrn.devgalop.securelib.Webapi/lrn.devgalop.securelib.Webapi.csproj" -c Release -o /app/build

#publish stage
FROM build AS publish
RUN dotnet publish "lrn.devgalop.securelib.Webapi/lrn.devgalop.securelib.Webapi.csproj" -c Release -o /app/publish

#exec stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "lrn.devgalop.securelib.Webapi.dll"]