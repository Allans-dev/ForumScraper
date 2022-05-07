#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.




FROM ubuntu:latest
RUN apt update -y
RUN apt install -y wget unzip
RUN wget -q —nv https://dl.google.com/linux/deb/pool/main/g/google-chrome-stable/google-chrome-stable_101.0.4951.54-1_amd64.deb
RUN dpkg -i google-chrome-stable_101.0.4951.54-1_amd64.deb
RUN apt -f install -y
RUN wget —nv -O chromedriver_linux64.zip https://chromedriver.storage.googleapis.com/index.html?path=101.0.4951.41/chromedriver_linux64.zip
ADD chromedriver_linux64.zip /app/bin/Release/net5.0/
RUN unzip chromedriver_linux64.zip

ENV PATH=/app/bin/Release/net5.0/


FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["ForumScraper/ForumScraper.csproj", "ForumScraper/"]
#RUN dotnet restore "ForumScraper/ForumScraper.csproj"
#COPY . .
#WORKDIR "/src/ForumScraper"
##RUN wget — no-verbose -O chromedriver_linux64.zip https://chromedriver.storage.googleapis.com/index.html?path=101.0.4951.41/chromedriver_linux64.zip
##RUN unzip chromedriver_linux64.zip
##ENV PATH=$PATH:./chromedriver
#RUN dotnet build "ForumScraper.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ForumScraper.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ForumScraper.dll"]