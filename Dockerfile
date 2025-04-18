FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY IntegrationTask.sln ./

COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY Web/Web.csproj ./Web/
COPY Application.Test/Application.Test.csproj ./Application.Test/
COPY Architecture.Test/Architecture.Test.csproj ./Architecture.Test/

RUN dotnet restore

COPY . .

WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Web.dll"]
