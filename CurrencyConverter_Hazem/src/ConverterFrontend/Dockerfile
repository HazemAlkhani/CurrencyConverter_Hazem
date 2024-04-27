FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ConverterFrontend.csproj", "./"]
RUN dotnet restore "ConverterFrontend.csproj"
COPY . .
WORKDIR "/src/ConverterFrontend"
RUN dotnet build "ConverterFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConverterFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConverterFrontend.dll"]