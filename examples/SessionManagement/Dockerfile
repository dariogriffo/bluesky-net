FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["examples/SessionManagement/SessionManagement.csproj", "examples/SessionManagement/"]
RUN dotnet restore "examples/SessionManagement/SessionManagement.csproj"
COPY . .
WORKDIR "/src/examples/SessionManagement"
RUN dotnet build "SessionManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SessionManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SessionManagement.dll"]
