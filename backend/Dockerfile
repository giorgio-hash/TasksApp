FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy and publish app and libraries
COPY . .


RUN dotnet publish -c Release -o out 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
ENV ASPNETCORE_URLS=http://+:5054
RUN addgroup app && adduser --system app
RUN addgroup app app 
USER app
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "backend.dll"]