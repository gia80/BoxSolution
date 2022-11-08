FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["YlibkaBox.Api/YlibkaBox.Api.csproj", "YlibkaBox.Api/"]
RUN dotnet restore "YlibkaBox.Api/YlibkaBox.Api.csproj"
COPY . .
WORKDIR "/src/YlibkaBox.Api"
RUN dotnet build "YlibkaBox.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YlibkaBox.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YlibkaBox.Api.dll"]
