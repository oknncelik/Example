FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Example.Api/Example.Api.csproj", "Example.Api/"]
COPY ["Example.Business/Example.Business.csproj", "Example.Business/"]
COPY ["Example.Common/Example.Common.csproj", "Example.Common/"]
COPY ["Example.Core/Example.Core.csproj", "Example.Core/"]
COPY ["Example.Dal/Example.Dal.csproj", "Example.Dal/"]
COPY ["Example.Entities/Example.Entities.csproj", "Example.Entities/"]
RUN dotnet restore "Example.Api/Example.Api.csproj"
COPY . .
WORKDIR "/src/Example.Api"
RUN dotnet build "Example.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Ensure the XML documentation file is present even if build fails to generate it
RUN touch Example.Api.xml
ENTRYPOINT ["dotnet", "Example.Api.dll"]
