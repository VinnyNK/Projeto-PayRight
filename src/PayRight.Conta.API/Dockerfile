FROM mcr.microsoft.com/dotnet/aspnet:6.0.1 AS base
WORKDIR /app
EXPOSE 5020
EXPOSE 5021

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./PayRight.Conta.API/PayRight.Conta.API.csproj", "PayRight.Conta.API/"]
COPY ["./PayRight.Conta.Domain/PayRight.Conta.Domain.csproj", "PayRight.Conta.Domain/"]
COPY ["./PayRight.Conta.Infra/PayRight.Conta.Infra.csproj", "PayRight.Conta.Infra/"]
COPY ["./PayRight.Shared/PayRight.Shared.csproj", "PayRight.Shared/"]

RUN dotnet restore "PayRight.Conta.API/PayRight.Conta.API.csproj"
COPY . .    
WORKDIR "PayRight.Conta.API"
RUN dotnet build "PayRight.Conta.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "PayRight.Conta.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayRight.Conta.API.dll"]