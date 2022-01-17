FROM mcr.microsoft.com/dotnet/aspnet:6.0.1 AS base
WORKDIR /app
EXPOSE 5010
EXPOSE 5011

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./PayRight.Cadastro.API/PayRight.Cadastro.API.csproj", "PayRight.Cadastro.API/"]
COPY ["./PayRight.Cadastro.Domain/PayRight.Cadastro.Domain.csproj", "PayRight.Cadastro.Domain/"]
COPY ["./PayRight.Cadastro.Infra/PayRight.Cadastro.Infra.csproj", "PayRight.Cadastro.Infra/"]
COPY ["./PayRight.Shared/PayRight.Shared.csproj", "PayRight.Shared/"]

RUN dotnet restore "PayRight.Cadastro.API/PayRight.Cadastro.API.csproj"
COPY . .    
WORKDIR "PayRight.Cadastro.API"
RUN dotnet build "PayRight.Cadastro.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PayRight.Cadastro.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayRight.Cadastro.API.dll"]