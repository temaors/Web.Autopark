FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web_Autopark/Web_Autopark.csproj", "Web_Autopark/"]
RUN dotnet restore "Web_Autopark/Web_Autopark.csproj"
COPY . .
WORKDIR "/src/Web_Autopark"
RUN dotnet build "Web_Autopark.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web_Autopark.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web_Autopark.dll"]
