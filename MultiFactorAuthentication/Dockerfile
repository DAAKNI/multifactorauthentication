#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-focal AS build
WORKDIR /src
COPY ["MultiFactorAuthentication.Web/MultiFactorAuthentication.Web.csproj", "MultiFactorAuthentication.Web/"]
RUN dotnet restore "MultiFactorAuthentication.Web/MultiFactorAuthentication.Web.csproj"
COPY . .
WORKDIR "/src/MultiFactorAuthentication.Web"
RUN dotnet build "MultiFactorAuthentication.Web.csproj" -c Release -o /app/build

FROM build AS debug
RUN dotnet publish "MultiFactorAuthentication.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiFactorAuthentication.Web.dll"]


