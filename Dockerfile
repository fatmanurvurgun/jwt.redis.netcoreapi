FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source
EXPOSE 80

COPY *.sln .
COPY jwt.redis.netcoreapi/*.csproj ./jwt.redis.netcoreapi/
RUN dotnet restore

COPY jwt.redis.netcoreapi/. ./jwt.redis.netcoreapi/
WORKDIR /source/jwt.redis.netcoreapi
RUN dotnet publish -c release -o /app --no-cache /restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet","jwt.redis.netcoreapi.dll"]




