#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Server/Elsa.Server/Elsa.Server.csproj", "src/Server/Elsa.Server/"]
RUN dotnet restore "src/Server/Elsa.Server/Elsa.Server.csproj"
COPY . .
WORKDIR /src
COPY ["src/Server/Elsa.Server/Elsa.Server.csproj", "src/Server/Elsa.Server/"]
RUN dotnet build "src/Server/Elsa.Server/Elsa.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Server/Elsa.Server/Elsa.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elsa.Server.dll"]