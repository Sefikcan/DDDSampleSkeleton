FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Sample.Presentation/Sample.Presentation.csproj", "src/Sample.Presentation/"]
RUN dotnet restore "src/Sample.Presentation/Sample.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Sample.Presentation"
RUN dotnet build "Sample.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Presentation.dll"]
