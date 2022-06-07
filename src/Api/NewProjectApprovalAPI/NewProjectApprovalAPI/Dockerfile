#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Api/NewProjectApprovalAPI/NewProjectApprovalAPI/NewProjectApprovalAPI.csproj", "src/Api/NewProjectApprovalAPI/NewProjectApprovalAPI/"]
COPY ["src/Api/NewProjectApprovalAPI/NewProjectApproval.IRepositories/NewProjectApproval.IRepositories.csproj", "src/Api/NewProjectApprovalAPI/NewProjectApproval.IRepositories/"]
COPY ["src/Api/NewProjectApprovalAPI/NewProjectApproval.Entities/NewProjectApproval.Entities.csproj", "src/Api/NewProjectApprovalAPI/NewProjectApproval.Entities/"]
COPY ["src/Api/NewProjectApprovalAPI/NewProjectApproval.Repositories/NewProjectApproval.Repositories.csproj", "src/Api/NewProjectApprovalAPI/NewProjectApproval.Repositories/"]
RUN dotnet restore "src/Api/NewProjectApprovalAPI/NewProjectApprovalAPI/NewProjectApprovalAPI.csproj"
COPY . .
WORKDIR "/src/src/Api/NewProjectApprovalAPI/NewProjectApprovalAPI"
RUN dotnet build "NewProjectApprovalAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewProjectApprovalAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewProjectApprovalAPI.dll"]