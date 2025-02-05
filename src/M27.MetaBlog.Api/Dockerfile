FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["M27.MetaBlog.Api/M27.MetaBlog.Api.csproj", "M27.MetaBlog.Api/"]
RUN dotnet restore "M27.MetaBlog.Api/M27.MetaBlog.Api.csproj"
COPY . .
WORKDIR "/src/M27.MetaBlog.Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "M27.MetaBlog.Api.dll"]
