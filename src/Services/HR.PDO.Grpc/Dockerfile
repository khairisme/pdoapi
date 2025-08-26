FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV GRPC_TOOLS_CPU_ARCH=x64
COPY ["src/Services/HR.Grpc/HR.Grpc.csproj", "src/Services/HR.Grpc/"]
COPY ["src/Services/HR.PDO.Core/HR.PDO.Core.csproj", "src/Services/HR.PDO.Core/"]
COPY ["src/Services/HR.PDO.Infrastructure/HR.PDO.Infrastructure.csproj", "src/Services/HR.PDO.Infrastructure/"]
COPY ["src/Shared/Shared.Contracts/Shared.Contracts.csproj", "src/Shared/Shared.Contracts/"]
COPY ["src/Shared/Shared.Messaging/Shared.Messaging.csproj", "src/Shared/Shared.Messaging/"]
RUN dotnet restore "src/Services/HR.Grpc/HR.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/HR.Grpc"
RUN dotnet build "HR.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.Grpc.dll"]
