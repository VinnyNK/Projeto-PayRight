FROM mcr.microsoft.com/dotnet/aspnet:6.0.1 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./PayRight.Autenticacao.API/PayRight.Autenticacao.API.csproj", "PayRight.Autenticacao.API/"]
COPY ["./PayRight.Shared/PayRight.Shared.csproj", "PayRight.Shared/"]

RUN dotnet restore "PayRight.Autenticacao.API/PayRight.Autenticacao.API.csproj"
COPY . .    
WORKDIR "PayRight.Autenticacao.API"
RUN dotnet build "PayRight.Autenticacao.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PayRight.Autenticacao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayRight.Autenticacao.API.dll"]