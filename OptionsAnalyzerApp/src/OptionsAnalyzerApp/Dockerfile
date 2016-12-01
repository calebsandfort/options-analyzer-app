FROM microsoft/dotnet:1.0.1-sdk-projectjson-nanoserver
# FROM microsoft/dotnet:1.0.1-sdk-projectjson

MAINTAINER Caleb Sandfort

COPY . /app

WORKDIR /app

RUN ["dotnet", "restore"]

EXPOSE 5000

ENTRYPOINT ["dotnet", "run"]