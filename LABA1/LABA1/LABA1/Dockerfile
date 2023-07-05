FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LABA1/LABA1.csproj", "LABA1/"]
RUN dotnet restore "LABA1/LABA1.csproj"
COPY . .
WORKDIR "/src/LABA1"
RUN dotnet build "LABA1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LABA1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LABA1.dll"]
