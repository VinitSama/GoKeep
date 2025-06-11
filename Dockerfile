FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GoKeep-june/GoKeep-june.csproj","GoKeep-june/"]
COPY ["GoKeep.Business/GoKeep.Business.csproj","GoKeep.Business/"]
COPY ["GoKeep.Model/GoKeep.Model.csproj","GoKeep.Model/"]
COPY ["GoKeep.Repository/GoKeep.Repository.csproj","GoKeep.Repository/"]
RUN dotnet restore "GoKeep-june/GoKeep-june.csproj"
COPY . .
WORKDIR "/src/GoKeep-june"
RUN dotnet build "GoKeep-june.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GoKeep-june.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "GoKeep-june.dll" ]