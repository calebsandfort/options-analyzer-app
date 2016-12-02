# FROM microsoft/dotnet:1.1.0-sdk-projectjson-nanoserver
# FROM microsoft/dotnet:1.1.0-sdk-projectjson
FROM microsoft/aspnetcore-build:1.1.0-projectjson

MAINTAINER Caleb Sandfort

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

EXPOSE 5000

ENTRYPOINT ["dotnet", "run"]