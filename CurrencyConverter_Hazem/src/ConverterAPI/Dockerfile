FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ConverterAPI/ConverterAPI.csproj", "ConverterAPI/"]


RUN dotnet restore "ConverterAPI/ConverterAPI.csproj"
COPY . .
WORKDIR "/src/ConverterAPI"
RUN dotnet build "ConverterAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConverterAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConverterAPI.dll"]
