FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY *.sln .
COPY GuitarCommerceAPI/*.csproj ./GuitarCommerceAPI/
RUN dotnet restore

COPY GuitarCommerceAPI/. ./GuitarCommerceAPI/
WORKDIR /app/GuitarCommerceAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5180

RUN chmod +x entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]