#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["grpc-example/grpc-example.csproj", "grpc-example/"]
RUN dotnet restore "grpc-example/grpc-example.csproj"
COPY . .
WORKDIR "/src/grpc-example"
RUN dotnet build "grpc-example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grpc-example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grpc-example.dll"]