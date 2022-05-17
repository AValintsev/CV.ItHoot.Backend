#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#PUPPETEER RECIPE
#####################
RUN apt-get update && apt-get -f install && apt-get -y install wget gnupg2 apt-utils
RUN wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add -
RUN echo 'deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main' >> /etc/apt/sources.list
RUN apt-get update \
&& apt-get install -y google-chrome-stable --no-install-recommends --allow-downgrades fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf
######################
#END PUPPETEER RECIPE
######################
ENV PUPPETEER_EXECUTABLE_PATH "/usr/bin/google-chrome-stable"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CVBuilder.Web/CVBuilder.Web.csproj", "src/CVBuilder.Web/"]
COPY ["src/DataLayer/CVBuilder.Models/CVBuilder.Models.csproj", "src/DataLayer/CVBuilder.Models/"]
COPY ["src/CVBuilder.Application/CVBuilder.Application.csproj", "src/CVBuilder.Application/"]
COPY ["src/DataLayer/CVBuilder.Repository/CVBuilder.Repository.csproj", "src/DataLayer/CVBuilder.Repository/"]
COPY ["src/DataLayer/CVBuilder.EFContext/CVBuilder.EFContext.csproj", "src/DataLayer/CVBuilder.EFContext/"]
RUN dotnet restore "src/CVBuilder.Web/CVBuilder.Web.csproj"
COPY . .
WORKDIR "/src/src/CVBuilder.Web"
RUN dotnet build "CVBuilder.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CVBuilder.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CVBuilder.Web.dll"]