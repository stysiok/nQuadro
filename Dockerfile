FROM mcr.microsoft.com/dotnet/sdk:6.0 as publish
WORKDIR /app
COPY ./src/modules ./src/modules
COPY ./src/nQuadro.shared ./src/nQuadro.shared
COPY ./src/nQuadro.entrypoint ./src/nQuadro.entrypoint

RUN dotnet publish src/nQuadro.entrypoint -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/out .

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

ENTRYPOINT dotnet nQuadro.entrypoint.dll