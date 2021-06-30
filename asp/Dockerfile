#FROM celebrater64/dotnet:latest


#https://docs.microsoft.com/ja-jp/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-5.0

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
#WORKDIR /asp/app
#COPY published/aspnetapp.dll ./
#ENTRYPOINT ["dotnet", "aspnetapp.dll"]


# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /asp/source

# copy csproj and restore as distinct layers
COPY aspnetapp/*.sln .
#COPY ApiTestKk/*.sln .
#COPY aspnetapp/*.csproj ./aspnetapp/
COPY aspnetapp/*.csproj ./aspnetapp/
#COPY ApiTestKk/*.csproj ./ApiTestKk/
#COPY ApiTestKk/*.csproj ./aspnetapp/
RUN dotnet restore

# copy everything else and build app
COPY aspnetapp/. ./aspnetapp/
WORKDIR /asp/source/aspnetapp
#COPY ApiTestKk/. ./ApiTestKk/
#COPY ApiTestKk/. ./aspnetapp/
#WORKDIR /asp/source/ApiTestKk
RUN dotnet publish -c release -o /asp/app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /asp/app
COPY --from=build /asp/app ./
#ENTRYPOINT ["dotnet", "aspnetapp.dll"]
ENTRYPOINT ["dotnet", "ApiTestKk.dll"]