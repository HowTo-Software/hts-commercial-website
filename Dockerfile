# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY HowToSoftware.Site.csproj ./
RUN dotnet restore HowToSoftware.Site.csproj
COPY . .
RUN dotnet publish HowToSoftware.Site.csproj -c Release --no-restore -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
ARG SOURCE_REVISION=unknown
LABEL org.opencontainers.image.title="hts-commercial-website" \
      org.opencontainers.image.source="https://github.com/HowTo-Software/hts-commercial-website" \
      org.opencontainers.image.revision="${SOURCE_REVISION}"
WORKDIR /app
# Pre-create the Data Protection key folder so a mounted named volume inherits the non-root owner.
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && chown -R app:app /home/app/.aspnet
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
USER app
ENTRYPOINT ["dotnet", "HowToSoftware.Site.dll"]
