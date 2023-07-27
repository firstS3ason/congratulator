#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApplication5LVL.TestAPI.csproj", "WebApplication5LVL.TestAPI/"]
COPY ["WebApplication5LVL.DataAccess.csproj", "WebApplication5LVL.DataAccess/"]
COPY ["WebApplication5LVL.AppData.csproj", "WebApplication5LVL.AppData/"]
COPY ["WebApplication5LVL.Contracts.csproj", "WebApplication5LVL.Contracts/"]
COPY ["WebApplication5LVL.Domain.csproj", "WebApplication5LVL.Domain/"]
COPY ["WebApplication5LVL.Infrastructure.csproj", "WebApplication5LVL.Infrastructure/"]
COPY ["WebApplication5LVL.Register.csproj", "WebApplication5LVL.Register/"]
RUN dotnet restore "WebApplication5LVL.TestAPI/WebApplication5LVL.TestAPI.csproj"
COPY . .
WORKDIR "/src/WebApplication5LVL.TestAPI"
RUN dotnet build "WebApplication5LVL.TestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication5LVL.TestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication5LVL.TestAPI.dll"]