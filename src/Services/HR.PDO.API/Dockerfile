FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV GRPC_TOOLS_CPU_ARCH=x64
COPY ["src/Services/HR.PDO.API/HR.PDO.API.csproj", "src/Services/HR.PDO.API/"]
COPY ["src/Services/HR.PDO.Application/HR.PDO.Application.csproj", "src/Services/HR.PDO.Application/"]
COPY ["src/Services/HR.PDO.Core/HR.PDO.Core.csproj", "src/Services/HR.PDO.Core/"]
COPY ["src/Services/HR.PDO.Infrastructure/HR.PDO.Infrastructure.csproj", "src/Services/HR.PDO.Infrastructure/"]
COPY ["src/Services/HR.PDO.Shared/HR.PDO.Shared.csproj", "src/Services/HR.PDO.Shared/"]
COPY ["src/Shared/Shared.Common/Shared.Common.csproj", "src/Shared/Shared.Common/"]
COPY ["src/Shared/Shared.Contracts/Shared.Contracts.csproj", "src/Shared/Shared.Contracts/"]
COPY ["src/Shared/Shared.Messaging/Shared.Messaging.csproj", "src/Shared/Shared.Messaging/"]
RUN dotnet restore "src/Services/HR.PDO.API/HR.PDO.API.csproj"
COPY . .
WORKDIR "/src/src/Services/HR.PDO.API"
RUN dotnet build "HR.PDO.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.PDO.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.PDO.API.dll"]
