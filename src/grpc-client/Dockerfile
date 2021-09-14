#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["grpc-client/grpc-client.csproj", "grpc-client/"]
RUN dotnet restore "grpc-client/grpc-client.csproj"
COPY . .
WORKDIR "/src/grpc-client"
RUN dotnet build "grpc-client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grpc-client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grpc-client.dll"]