#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
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